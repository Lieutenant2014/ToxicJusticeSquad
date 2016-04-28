package improbable.natures

import improbable.behaviours.player._
import improbable.behaviours.player.controls.{DelegatePlayerStatesBehaviour, MakeClientSideAuthorative, PlayerInteractorBehaviour}
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelib.util.EntityOwner
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.entity.physical.FreezeConstraints
import improbable.entity.physical.RigidbodyDataData.{CollisionDetectionMode, InterpolationMode}
import improbable.math.Vector3d
import improbable.papi.engine.EngineId
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.person.PersonState
import improbable.player.LocalPlayerCheckState
import improbable.player.controls.{PlacementState, PlayerControlsState}
import improbable.player.notification.{NotificationState, ScoreState}
import improbable.player.physical.PlayerState
import improbable.util.EntityPrefabs._

object PlayerNature extends NatureDescription {

  val NUMBER_OF_SKINS = 5

  override def dependencies: Set[NatureDescription] = Set(BaseNature, TransformNature, RigidbodyNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = {
    Set(
      descriptorOf[DelegatePlayerStatesBehaviour],
      descriptorOf[MakeClientSideAuthorative],
      descriptorOf[PlaceObjectBehaviour],
      descriptorOf[ForceTeleportBehaviour],
      descriptorOf[ForwardNotificationsBehaviour],
      descriptorOf[ForwardScoreBehaviour],
      descriptorOf[PlayerInteractorBehaviour],
      descriptorOf[PlayerMounterBehaviour],
      descriptorOf[SpawnSudBehaviour]
    )
  }

  def apply(engineId: EngineId): NatureApplication = {
    application(
      states = Seq(
        EntityOwner(ownerId = Some(engineId)),
        PlayerState(forceMagnitude = 20.0f),
        PersonState(Math.floor(Math.random() * (NUMBER_OF_SKINS - 1)).toInt),
        PlayerControlsState(movementDirection = Vector3d.zero, fireDirection = Vector3d.zero),
        LocalPlayerCheckState(),
        PlacementState(),
        NotificationState(),
        ScoreState(Map.empty)
      ),
      natures = Seq(
        BaseNature(entityPrefab = PLAYER),
        TransformNature(Vector3d(0, 0.5, 0)),
        RigidbodyNature(rotationConstraints = FreezeConstraints(x = true, y = true, z = true), drag = 0, interpolationMode = InterpolationMode.Interpolate, collisionMode = CollisionDetectionMode.Continuous)
      )
    )
  }

}
