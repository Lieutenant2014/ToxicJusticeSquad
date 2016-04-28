package improbable.natures

import improbable.behaviours.infection.SlimeBehaviour
import improbable.corelib.natures.{NatureApplication, BaseNature, NatureDescription}
import improbable.corelibrary.transforms.TransformNature
import improbable.infection.SlimeState
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs._

/**
  * Created by rob on 09/04/2016.
  */
object SlimeNature extends NatureDescription {
  override def dependencies = Set[NatureDescription](BaseNature, TransformNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[SlimeBehaviour]
  )

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        SlimeState(1)
      ),
      natures = Seq(
        BaseNature(entityPrefab = SLIME),
        TransformNature(initialPosition.toVector3d)
      )
    )
  }
}
