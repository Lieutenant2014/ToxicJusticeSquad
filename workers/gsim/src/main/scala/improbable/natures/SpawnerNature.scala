package improbable.natures

import improbable.behaviours.spawner.SpawnBehaviour
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelib.util.EntityOwner
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.math.{Coordinates, Vector3d}
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.player.LocalPlayerCheckState
import improbable.player.controls.{PlacementState, PlayerControlsState}
import improbable.player.physical.PlayerState
import improbable.spawner.SpawnSate
import improbable.util.EntityPrefabs.BASE

object SpawnerNature extends NatureDescription {

  override def dependencies: Set[NatureDescription] = Set(BaseNature, TransformNature, RigidbodyNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[SpawnBehaviour]
  )

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        SpawnSate(1)
      ),
      natures = Seq(
        BaseNature(entityPrefab = BASE),
        TransformNature(initialPosition.toVector3d),
        RigidbodyNature()
      )
    )
  }

}
