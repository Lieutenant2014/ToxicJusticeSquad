package improbable.behaviours.player

import improbable.natures.SudNature
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.player.controls.PlayerControlsState

/**
  * Created by thopau on 09/04/2016.
  */
class SpawnSudBehaviour(entity: Entity, world: World) extends EntityBehaviour {
  override def onReady(): Unit = {
    entity.watch[PlayerControlsState].onSpawnSud{ sudPos =>
      world.entities.spawnEntity(SudNature(sudPos.sudPos))
    }
  }
}
