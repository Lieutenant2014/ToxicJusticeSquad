package improbable.behaviours.player

import improbable.papi.entity.EntityBehaviour
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg
import improbable.player.notification.ScoreStateWriter

case class SendScore(values: Map[String, String]) extends CustomMsg

class ForwardScoreBehaviour(world: World,
                            scores: ScoreStateWriter) extends EntityBehaviour {

  world.messaging.onReceive {
    case SendScore(newScores) =>
      scores.update.values(newScores).finishAndSend()
  }

}