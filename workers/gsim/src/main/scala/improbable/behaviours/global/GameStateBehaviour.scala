package improbable.behaviours.global

import improbable.apps.{CityBlockUtils, ForceTeleportAllPlayers, PlayerLifeCycleManager}
import improbable.apps.gamecontroller._
import improbable.behaviours.player.ForceTeleport
import improbable.global.{GameStateWriter, MapEntity}
import improbable.math.Coordinates
import improbable.natures.{EvacuationPointNature, HumveeNature, LeakNature, MinionNature}
import improbable.papi.EntityId
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.util.EntityPrefabs
import org.flagz.{FlagInfo, ScalaFlagz}

import scala.concurrent.duration._

object GameStateSettings {

  @FlagInfo(name = "max_minions", help = "Number of minions.")
  final val MAX_MINIONS = ScalaFlagz.valueOf(100)


  @FlagInfo(name = "round_length", help = "Length of round.")
  final val ROUND_LENGTH = ScalaFlagz.valueOf(60)
}

class GameStateBehaviour(world: World, state: GameStateWriter, entity: Entity) extends EntityBehaviour {
  private val incrementInterval = 1.0.seconds

  entity.addTag("Global")

  val prefabsToClear = Set(
    EntityPrefabs.SLIME,
    EntityPrefabs.BARREL,
    EntityPrefabs.CRATE,
    EntityPrefabs.EVACUATION_POINT,
    EntityPrefabs.MINION,
    EntityPrefabs.SUD,
    EntityPrefabs.HUMVEE
  )

  val humveeSpawnPositions = Seq(
    Coordinates(28, 1 ,31),
    Coordinates(-50, 1 ,-131),
    Coordinates(50, 1 ,-71)
  )
  var humveeSpawnIndex = 0

  def minionSpawned(entityId: EntityId): Unit = {
    state.update.minionCount(state.minionCount + 1).finishAndSend()
  }
  def minionSaved(entityId: EntityId): Unit = {
    state.update.minionCount(state.minionCount - 1).score(state.score + 1).finishAndSend()
    Notifier.sendNotification(world, "Notification", "Saved!")
  }
  def minionCleaned(entityId: EntityId) : Unit = {
    state.update.minionCount(0).score(0).finishAndSend()
  }

  def restartGame() = {
    Notifier.sendNotification(world, "Game Over", "Score: "+state.score)
    Notifier.sendNotification(world, "Game Restarting...", "")

    world.entities.find(Coordinates(0,0,0), Double.MaxValue, Set.empty).foreach {
      entity =>
        if(prefabsToClear.contains(entity.prefab)) {

          if(entity.prefab == EntityPrefabs.HUMVEE){
            world.messaging.sendToEntity(entity.entityId, ForceTeleport(humveeSpawnPositions(humveeSpawnIndex)))
            humveeSpawnIndex = (humveeSpawnIndex + 1) % humveeSpawnPositions.length
          } else {
            world.entities.destroyEntity(entity.entityId)
          }
        }
    }

    world.entities.spawnEntity(EvacuationPointNature(Coordinates(90.0d, 0, 90.0d)))
    world.entities.spawnEntity(EvacuationPointNature(Coordinates(-150.0d, 0, 90.0d)))
    world.entities.spawnEntity(EvacuationPointNature(Coordinates(90.0d, 0, -150.0d)))
    world.entities.spawnEntity(EvacuationPointNature(Coordinates(-150.0d, 0, -150.0d)))

    for(i <- 1 to GameStateSettings.MAX_MINIONS.get.toInt) {
      world.timing.after(i.milliseconds) {
        val spawnedMinion = world.entities.spawnEntity(MinionNature(CityBlockUtils.randomBlockPosition(height=2.0)))
        minionSpawned(spawnedMinion)
      }
    }

    world.messaging.sendToApp(classOf[PlayerLifeCycleManager].getCanonicalName, ForceTeleportAllPlayers())

    state.update.score(0).minionCount(0).mapEntities(List.empty).time(GameStateSettings.ROUND_LENGTH.get.toInt).finishAndSend()
  }

  override def onReady(): Unit = {

    restartGame()

    humveeSpawnPositions.map(coords => {
      world.entities.spawnEntity(HumveeNature(None, coords))
    })

    world.messaging.onReceive {
      case PlayerJoined(player) =>
        Notifier.sendNotification(world, "", s"Player Joined $player")

      case PlayerLeft(player) =>
        Notifier.sendNotification(world, "Notification", s"Player Joined $player")

      case MinionReachedEvacuation(minion) =>
        minionSaved(minion)

      case MinionCleanedUp(minion) =>
        minionCleaned(minion)
    }

    world.timing.every(incrementInterval) {
      val newTime = state.time - 1
      if(newTime <= 0) {
        restartGame()
      } else {

        if(newTime % 2 == 0){

          val playerEntities = world.entities.find(Coordinates(0,0,0), Double.MaxValue, Set("improbable.natures.PlayerNature")).map(entity => {
            MapEntity(entity.entityId, "Player", entity.position)
          })

          val barrelEntities = world.entities.find(Coordinates(0,0,0), Double.MaxValue, Set("improbable.natures.LeakNature")).map(entity => {
            MapEntity(entity.entityId, "Barrel", entity.position)
          })

          state.update.mapEntities((playerEntities ++ barrelEntities).toList).finishAndSend()
        }

        if(newTime % 10 == 0){
          world.entities.spawnEntity(LeakNature(CityBlockUtils.randomBlockPosition(height=10.0)))
        }

        state.update.time(newTime).finishAndSend()
      }
    }
  }
}