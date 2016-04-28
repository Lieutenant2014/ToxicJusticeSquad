package improbable.apps.gamecontroller

import improbable.apps.PlayerLifeCycleManager
import improbable.behaviours.player.SendNotification
import improbable.papi.world.World

object Notifier {
  def sendNotification(world: World, title: String, message: String): Unit = {
    world.messaging.sendToApp(classOf[PlayerLifeCycleManager].getCanonicalName, SendNotification(title, message))
  }
}
