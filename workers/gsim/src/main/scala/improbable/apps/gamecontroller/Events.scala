package improbable.apps.gamecontroller

import improbable.papi.EntityId
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg

trait Event extends CustomMsg
case class PlayerJoined(name: String) extends Event
case class PlayerLeft(name: String) extends Event
case class MinionReachedEvacuation(minion: EntityId) extends Event
case class MinionCleanedUp(minion: EntityId) extends Event

object Events {
  def recordEvent(world: World,
                  event: Event): Unit = {
    world.messaging.sendToApp(classOf[GameController].getCanonicalName, event)
  }
}
