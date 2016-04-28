package improbable.natures

import improbable.behaviours.infection.SlimeBehaviour
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.infection.SlimeState
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs._

/**
  * Created by rob on 08/04/2016.
  */
object LeakNature extends NatureDescription {

  override def dependencies = Set[NatureDescription](BaseNature, TransformNature, RigidbodyNature, InfectionNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[SlimeBehaviour]
  )

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        SlimeState(1)
      ),
      natures = Seq(
        BaseNature(entityPrefab = BARREL),
        TransformNature(initialPosition.toVector3d),
        RigidbodyNature(drag = 0.2f, mass = 1.5f),
        InfectionNature(true)
      )
    )
  }
}
