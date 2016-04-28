package improbable.behaviours.player.controls

import improbable.corelib.util.EntityOwnerDelegation.entityOwnerDelegation
import improbable.corelib.util.EntityOwnerWriter
import improbable.corelibrary.transforms.TransformInterface
import improbable.entity.physical.PhysicsSimulationController
import improbable.papi.EntityId
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg
import improbable.player.controls.VehicleControlsState

case class MountPlayer(IdToMountTo: EntityId) extends CustomMsg
case class UnmountPlayer(IdToUnmountFrom: EntityId) extends CustomMsg

class DelegateHumveeStatesBehaviour(entity: Entity, world: World, entityOwnerWriter: EntityOwnerWriter, transformInterface: TransformInterface, physicsController: PhysicsSimulationController) extends EntityBehaviour {

  override def onReady(): Unit = {

    world.messaging.onReceive {
      case PlayerInteraction(entityId, engineId, inCar) =>

        //println(s"Got player interaction $entityId $engineId $inCar")

        if(entityOwnerWriter.ownerId.isEmpty && !inCar)
        {
          //println("No owner, not in car")
          entityOwnerWriter.update.ownerId(Some(engineId)).finishAndSend()
          world.messaging.sendToEntity(entityId, MountPlayer(entity.entityId))
        }
        else if (entityOwnerWriter.ownerId.isDefined && !inCar)
        {
          //println("Owner, not in car")
          world.messaging.sendToEntity(entityId, MountPlayer(entity.entityId))
        }
        else if (entityOwnerWriter.ownerId.isDefined && entityOwnerWriter.ownerId.get == engineId && inCar)
        {
          //println("Owner, in car")
          entityOwnerWriter.update.ownerId(None).finishAndSend()
          world.messaging.sendToEntity(entityId, UnmountPlayer(entity.entityId))
        }
        else if (entityOwnerWriter.ownerId.isDefined && entityOwnerWriter.ownerId.get != engineId && inCar)
        {
          //println("owner, but not you, in car")
          world.messaging.sendToEntity(entityId, UnmountPlayer(entity.entityId))
        }
        else if (entityOwnerWriter.ownerId.isEmpty && inCar)
        {
          //println("No owner, in car")
          world.messaging.sendToEntity(entityId, UnmountPlayer(entity.entityId))
        }

    }

    entity.delegateStateToOwner[VehicleControlsState]
  }
}
