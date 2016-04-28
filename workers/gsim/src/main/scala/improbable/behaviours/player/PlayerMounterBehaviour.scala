package improbable.behaviours.player

import improbable.behaviours.player.controls.{UnmountPlayer, MountPlayer}
import improbable.corelib.math.Quaternion
import improbable.corelibrary.transforms.{Parent, TransformInterface}
import improbable.math.Vector3d
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World

import scala.util.Random

class PlayerMounterBehaviour(entity: Entity, world: World, transformInterface: TransformInterface) extends EntityBehaviour{

  override def onReady(): Unit = {
    world.messaging.onReceive{
      case MountPlayer(id) =>
        transformInterface.parent(newParent = Parent(id, Random.alphanumeric.take(10).mkString), Vector3d(0, 1, 0), Quaternion.identity)
      case UnmountPlayer(id) =>
        transformInterface.unparent()
    }
  }
}
