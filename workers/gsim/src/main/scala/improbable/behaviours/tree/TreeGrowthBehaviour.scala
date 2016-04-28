package improbable.behaviours.tree

import improbable.natures.TreeNature
import improbable.treeState.TreeState
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.unity.fabric.PhysicsEngineConstraint

class TreeGrowthBehaviour(world: World, entity : Entity) extends EntityBehaviour {

  override def onReady(): Unit = {
    entity.delegateState[TreeState](PhysicsEngineConstraint)

    entity.watch[TreeState].onSpawnTree(event => {
      world.entities.spawnEntity(TreeNature(event.spawnPosition))
    })
  }

}