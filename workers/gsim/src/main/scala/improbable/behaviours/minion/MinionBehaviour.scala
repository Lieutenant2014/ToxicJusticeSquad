package improbable.behaviours.minion

import improbable.Cancellable
import improbable.infection.InfectionState
import improbable.life.LifeStateWriter
import improbable.navigation.{SteeringSourceDataState, TargetVelocityState}
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.unity.fabric.PhysicsEngineConstraint

class MinionBehaviour(entity: Entity, world: World, life: LifeStateWriter) extends EntityBehaviour{

  override def onReady(): Unit = {
    entity.delegateState[TargetVelocityState](PhysicsEngineConstraint)
    entity.delegateState[SteeringSourceDataState](PhysicsEngineConstraint)
  }

  var cancelOpt = Option.empty[Cancellable]

  entity.watch[InfectionState].bind.infected {
    case true =>
      import scala.concurrent.duration._
      cancelOpt = Some(world.timing.after(20.seconds) {
        life.update.alive(false).finishAndSend()
      })
    case false =>
      cancelOpt.foreach(_.cancel())
  }


}
