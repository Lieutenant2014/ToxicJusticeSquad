package improbable.apps

import com.typesafe.scalalogging.Logger
import improbable.apps.PlayerLifeCycleManager._
import improbable.apps.gamecontroller.{Events, Notifier, PlayerJoined, PlayerLeft}
import improbable.behaviours.player.{ForceTeleport, SendNotification, SendScore}
import improbable.math.Coordinates
import improbable.natures.PlayerNature
import improbable.papi._
import improbable.papi.engine.EngineId
import improbable.papi.world.AppWorld
import improbable.papi.world.messaging.{CustomMsg, EngineConnected, EngineDisconnected}
import improbable.papi.worldapp.WorldApp

case class ForceTeleportAllPlayers() extends CustomMsg
case class EngineDisconnnected() extends CustomMsg

class PlayerLifeCycleManager(appWorld: AppWorld,
                             logger: Logger) extends WorldApp {

  private var userIdToEntityIdMap = Map[EngineId, EntityId]()
  private var entityIdToSpawnMap = Map[EntityId, Coordinates]()

  appWorld.messaging.subscribe {
    case engineConnectedMsg: EngineConnected =>
      engineConnected(engineConnectedMsg)

    case engineDisconnectedMsg: EngineDisconnected =>
      engineDisconnected(engineDisconnectedMsg)
  }

  appWorld.messaging.onReceive {
    case ForceTeleportAllPlayers() =>
      Notifier.sendNotification(appWorld, "", "To the start!")
      entityIdToSpawnMap.foreach {
        case (entity, position) =>
          appWorld.messaging.sendToEntity(entity, ForceTeleport(position))
      }

    case score: SendScore =>
      entityIdToSpawnMap.keys.foreach {
        case entity =>
          appWorld.messaging.sendToEntity(entity, score)
      }
  }

  appWorld.messaging.onReceive {
    case notify: SendNotification =>
      println(s"Notification: ${notify.title} ${notify.message}")
      userIdToEntityIdMap.values.foreach(appWorld.messaging.sendToEntity(_, notify))
  }

  private def engineConnected(msg: EngineConnected): Unit = {
    msg match {
      // For now use the engineName as the userId.
      case EngineConnected(userId, UNITY_CLIENT, _) =>
        addEntity(userId)
      case _ =>
    }
  }

  private def addEntity(userId: String): Unit = {
    Events.recordEvent(appWorld, PlayerJoined(userId))
    val playerEntityId = appWorld.entities.spawnEntity(PlayerNature(engineId = userId))
    logger.info(s"Spawning Player with userId $userId and entityId $playerEntityId")
    entityIdToSpawnMap += (playerEntityId -> Coordinates.zero)
    userIdToEntityIdMap += userId -> playerEntityId
  }

  private def engineDisconnected(msg: EngineDisconnected): Unit = {
    msg match {
      case EngineDisconnected(userId, UNITY_CLIENT) =>
        removeUserIdToEntityIdEntry(userId)
      case _ =>
    }
  }

  private def removeUserIdToEntityIdEntry(userId: EngineId) = {
    userIdToEntityIdMap.get(userId) match {
      case Some(id) =>
        appWorld.messaging.sendToEntity(id, EngineDisconnnected())
        appWorld.entities.destroyEntity(id)
        Events.recordEvent(appWorld, PlayerLeft(userId))
        logger.info(s"Destroying player: $userId with entityId $id")
      case None =>
        logger.warn(s"User disconnected but could not find entity id for player: $userId")
    }
  }
}

object PlayerLifeCycleManager {
  private val UNITY_CLIENT = "UnityClient"
}
