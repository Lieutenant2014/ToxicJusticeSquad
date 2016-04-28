package improbable.natures

import improbable.behaviours.infection.SlimeBehaviour
import improbable.behaviours.minion.MinionBehaviour
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.entity.physical.FreezeConstraints
import improbable.infection.SlimeState
import improbable.life.LifeState
import improbable.math.{Coordinates, Vector3d}
import improbable.navigation.SteeringSourceData.SteeringSource.SteeringSource
import improbable.navigation.{SteeringSourceDataState, TargetVelocityState, VelocityWeightPair}
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.person.PersonState
import improbable.util.EntityPrefabs.MINION

import scala.util.Random

object MinionNature extends NatureDescription {

  val NUMBER_OF_SKINS = 11

  override def dependencies = Set[NatureDescription](BaseNature, TransformNature, RigidbodyNature, ColoredNature, InfectionNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[MinionBehaviour],
    descriptorOf[SlimeBehaviour]
  )

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        TargetVelocityState(Vector3d.zero, maxAccel = 3),
        LifeState(true),
        SlimeState(0.0f),
        SteeringSourceDataState(Map[SteeringSource, VelocityWeightPair]()),
        PersonState(Math.floor(Math.random() * (NUMBER_OF_SKINS - 1)).toInt)
      ),
      natures = Seq(
        BaseNature(entityPrefab = MINION),
        TransformNature(initialPosition.toVector3d),
        RigidbodyNature(rotationConstraints = FreezeConstraints(x = true, y = true, z = true), mass = 3.0f),
        InfectionNature(false),
        ColoredNature(color = java.awt.Color.white))
    )
  }
}
