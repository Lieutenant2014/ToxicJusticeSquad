package improbable.behaviours.player

import improbable.papi.entity.EntityBehaviour
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg
import improbable.player.notification.NotificationStateWriter

/**
  * Created by rob on 08/04/2016.
  */

case class SendNotification(title: String, message: String) extends CustomMsg

class ForwardNotificationsBehaviour(world: World,
                                    notification: NotificationStateWriter) extends EntityBehaviour {

  world.messaging.onReceive {
    case SendNotification(title, message) =>
      notification.update.triggerNotificationEvent(title, message).finishAndSend()
  }

}
