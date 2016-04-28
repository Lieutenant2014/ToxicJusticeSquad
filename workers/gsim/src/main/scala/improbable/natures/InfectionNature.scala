package improbable.natures

import improbable.behaviours.infection.DelegateInfectionStatesBehaviour
import improbable.corelib.natures.{NatureApplication, NatureDescription}
import improbable.infection.InfectionState
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor

/**
  * Created by rob on 09/04/2016.
  */
object InfectionNature extends NatureDescription {

  override def dependencies = Set[NatureDescription]()

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[DelegateInfectionStatesBehaviour]
  )

  def apply(isInfected: Boolean): NatureApplication = {
    application(
      states = Seq(
        InfectionState(isInfected)
      ),
      natures = Seq()
    )
  }
}
