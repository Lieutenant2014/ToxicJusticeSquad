package improbable.behaviours.infection

import improbable.infection.InfectionState
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.unity.fabric.PhysicsEngineConstraint

class DelegateInfectionStatesBehaviour(entity: Entity) extends EntityBehaviour {
  entity.delegateState[InfectionState](PhysicsEngineConstraint)
}
