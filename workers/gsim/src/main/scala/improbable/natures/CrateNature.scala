package improbable.natures

import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs._

/**
  * Created by rob on 09/04/2016.
  */
object CrateNature extends NatureDescription {
  override def dependencies = Set[NatureDescription](BaseNature, TransformNature, RigidbodyNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
  )

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
      ),
      natures = Seq(
        BaseNature(entityPrefab = CRATE),
        TransformNature(initialPosition.toVector3d),
        RigidbodyNature(drag = 0.2f, mass = 5.0f)
      )
    )
  }
}
