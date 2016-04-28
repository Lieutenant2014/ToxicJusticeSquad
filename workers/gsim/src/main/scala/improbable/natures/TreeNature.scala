package improbable.natures

import improbable.behaviours.tree.TreeGrowthBehaviour
import improbable.treeState.TreeState
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs.TREE

object TreeNature extends NatureDescription {

  override def dependencies: Set[NatureDescription] = Set(BaseNature, TransformNature, RigidbodyNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = {
    Set(
      descriptorOf[TreeGrowthBehaviour]
    )
  }

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        TreeState(size = 0.1f)
      ),
      natures = Seq(
        BaseNature(entityPrefab = TREE),
        TransformNature(initialPosition.toVector3d),
        RigidbodyNature(isKinematic = true)
      )
    )
  }

}
