package improbable.natures

import improbable.behaviours.suds.DestroyYourselfAfterSomeTime
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.rigidbody.RigidbodyNature
import improbable.corelibrary.transforms.TransformNature
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs._

/**
  * Created by rob on 08/04/2016.
  */
object SudNature extends NatureDescription {

  override def dependencies = Set[NatureDescription](BaseNature, TransformNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[DestroyYourselfAfterSomeTime]
  )

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
      ),
      natures = Seq(
        BaseNature(entityPrefab = SUD),
        TransformNature(initialPosition.toVector3d)
      )
    )
  }
}
