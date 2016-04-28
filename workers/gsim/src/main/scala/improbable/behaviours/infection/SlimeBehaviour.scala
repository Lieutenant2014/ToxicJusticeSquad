package improbable.behaviours.infection

import improbable.apps.gamecontroller.{MinionCleanedUp, Events}
import improbable.infection.SlimeState
import improbable.natures.SlimeNature
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.unity.fabric.PhysicsEngineConstraint
import improbable.util.EntityPrefabs

/**
  * Created by rob on 09/04/2016.
  */
class SlimeBehaviour(entity: Entity, world: World)  extends EntityBehaviour{
  entity.delegateState[SlimeState](PhysicsEngineConstraint)

  entity.watch[SlimeState].onSpreadSlimeEvent {
    spreadSlimeEvent =>
      world.entities.spawnEntity(SlimeNature(spreadSlimeEvent.location))
  }

  entity.watch[SlimeState].onCleanSlimeEvent {
    cleanSlimeEvent =>
      if(entity.tags.contains(EntityPrefabs.MINION.name)) {
        Events.recordEvent(world, MinionCleanedUp(entity.entityId))
      }
      world.entities.destroyEntity(entity.entityId)
  }
}
