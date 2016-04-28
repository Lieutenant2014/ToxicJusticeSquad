package improbable.behaviours.evacuationpoint

import improbable.apps.gamecontroller.{MinionReachedEvacuation, Events}
import improbable.evacuation.EvacuateMinionState
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.unity.fabric.PhysicsEngineConstraint

class EvacuationPointBehaviour(entity: Entity,
                               world: World) extends EntityBehaviour {
  override def onReady(): Unit = {
    entity.delegateState[EvacuateMinionState](PhysicsEngineConstraint)
  }

  entity.watch[EvacuateMinionState].onEvacuateMinionEvent {
    minionEvent =>
      Events.recordEvent(world, MinionReachedEvacuation(minionEvent.minion))
      world.entities.destroyEntity(minionEvent.minion)
  }
}

