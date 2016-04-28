package improbable.behaviours.player.controls

import improbable.apps.EngineDisconnnected
import improbable.corelib.entity.nature.NatureData
import improbable.corelib.util.EntityOwner
import improbable.corelibrary.transforms.TransformInterface
import improbable.papi.EntityId
import improbable.papi.engine.EngineId
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg
import improbable.player.controls.PlayerControlsState

case class PlayerInteraction(entityId: EntityId, engineId: EngineId, hasParent: Boolean) extends CustomMsg

class PlayerInteractorBehaviour(entity: Entity, world: World, transformInterface: TransformInterface) extends EntityBehaviour {

  override def onReady(): Unit = {
    world.messaging.onReceive {
      case EngineDisconnnected() =>
        val isInCar = transformInterface.parent.isDefined
        if(isInCar) {
          world.messaging.sendToEntity(transformInterface.parent.get.parentId,
            PlayerInteraction(entity.entityId, entity.watch[EntityOwner].ownerId.get.get, hasParent = true)
          )
        }
    }

    entity.watch[PlayerControlsState].onPlayerInteract {
      interact =>
        val isInCar = transformInterface.parent.isDefined
        if(isInCar) {
          println("In car trying to leave: " + transformInterface.parent.get.parentId)
          world.messaging.sendToEntity(transformInterface.parent.get.parentId,
            PlayerInteraction(entity.entityId, entity.watch[EntityOwner].ownerId.get.get, hasParent = true)
          )
        } else {
          val thingsAround = world.entities.find(entity.position, 10.0f)
          val thingsAroundThatAreCar = thingsAround.find(thing => thing.get[NatureData].get.natures.contains("improbable.natures.HumveeNature"))
          thingsAround.foreach(thing => world.messaging.sendToEntity(thing.entityId,
            PlayerInteraction(entity.entityId, entity.watch[EntityOwner].ownerId.get.get, hasParent = false))
          )
        }
    }
  }
}
