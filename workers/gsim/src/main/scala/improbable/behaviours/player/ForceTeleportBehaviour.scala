package improbable.behaviours.player

import improbable.corelibrary.transforms.TransformInterface
import improbable.math.Coordinates
import improbable.papi.entity.EntityBehaviour
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg

case class ForceTeleport(position: Coordinates) extends CustomMsg

class ForceTeleportBehaviour(world: World,
                             transformInterface: TransformInterface) extends EntityBehaviour {
  world.messaging.onReceive {
    case ForceTeleport(position) =>
      println(s"Teleporting to $position")
      transformInterface.teleportPosition(position.toVector3d)
  }
}
