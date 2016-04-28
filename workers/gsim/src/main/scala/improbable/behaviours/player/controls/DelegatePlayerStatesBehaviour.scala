package improbable.behaviours.player.controls

import improbable.corelib.util.EntityOwnerDelegation.entityOwnerDelegation
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.player.LocalPlayerCheckState
import improbable.player.controls.{PlacementState, PlayerControlsState}

class DelegatePlayerStatesBehaviour(entity: Entity) extends EntityBehaviour {

  override def onReady(): Unit = {
    entity.delegateStateToOwner[PlayerControlsState]
    entity.delegateStateToOwner[PlacementState]
    entity.delegateStateToOwner[LocalPlayerCheckState]
  }
}
