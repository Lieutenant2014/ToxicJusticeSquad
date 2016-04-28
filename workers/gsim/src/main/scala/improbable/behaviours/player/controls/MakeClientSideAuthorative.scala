package improbable.behaviours.player.controls

import improbable.entity.physical.PhysicsSimulationController
import improbable.papi.entity.EntityBehaviour

/**
  * Created by thopau on 02/04/2016.
  */
class MakeClientSideAuthorative(physicsController: PhysicsSimulationController) extends EntityBehaviour {
  override def onReady(): Unit = {
    physicsController.useClientOwnerPhysics()
  }
}
