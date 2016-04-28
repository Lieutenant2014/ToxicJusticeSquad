package improbable.natures

import improbable.behaviours.evacuationpoint.EvacuationPointBehaviour
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.transforms.TransformNature
import improbable.evacuation.EvacuateMinionState
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs._

/**
  * Created by rob on 08/04/2016.
  */
object EvacuationPointNature extends NatureDescription {

  override def dependencies: Set[NatureDescription] = Set(BaseNature, TransformNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = {
    Set(
      descriptorOf[EvacuationPointBehaviour]
    )
  }

  def apply(initialPosition: Coordinates): NatureApplication = {
    application(
      states = Seq(
        EvacuateMinionState()
      ),
      natures = Seq(
        BaseNature(entityPrefab = EVACUATION_POINT),
        TransformNature(initialPosition.toVector3d)
      )
    )
  }
}
