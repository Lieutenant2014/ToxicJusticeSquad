package improbable.natures

import improbable.behaviours.player.ForceTeleportBehaviour
import improbable.behaviours.player.controls.DelegateHumveeStatesBehaviour
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelib.util.EntityOwner
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.entity.physical.FreezeConstraints
import improbable.math.{Coordinates, Vector3d}
import improbable.papi.engine.EngineId
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.player.controls.{PlayerControlsState, VehicleControlsState}
import improbable.util.EntityPrefabs._

object HumveeNature extends NatureDescription {

  override def dependencies: Set[NatureDescription] = Set(BaseNature, TransformNature, RigidbodyNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = {
    Set(
      descriptorOf[DelegateHumveeStatesBehaviour],
      descriptorOf[ForceTeleportBehaviour]
    )
  }

  def apply(engineId: Option[EngineId], initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        EntityOwner(ownerId = engineId),
        PlayerControlsState(movementDirection = Vector3d.zero, fireDirection = Vector3d.zero),
        VehicleControlsState(Vector3d.zero)
      ),
      natures = Seq(
        BaseNature(entityPrefab = HUMVEE),
        TransformNature(initialPosition.toVector3d),
        RigidbodyNature(drag = 5f, rotationConstraints = FreezeConstraints(x = true, y = true, z = true), mass = 100.0f)
      )
    )
  }
}
