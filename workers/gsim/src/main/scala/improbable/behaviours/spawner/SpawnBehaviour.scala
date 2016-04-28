package improbable.behaviours.spawner

import improbable.natures.MinionNature
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.spawner.SpawnSate
import improbable.unity.fabric.PhysicsEngineConstraint

/**
  * Created by thopau on 02/04/2016.
  */
class SpawnBehaviour(entity: Entity, world: World) extends EntityBehaviour{

  override def onReady(): Unit =
  {
    entity.delegateState[SpawnSate](PhysicsEngineConstraint)
    entity.watch[SpawnSate].onSpawnEntityEvent{
      evt =>
        world.entities.spawnEntity(MinionNature(evt.position));
    }
  }
}
