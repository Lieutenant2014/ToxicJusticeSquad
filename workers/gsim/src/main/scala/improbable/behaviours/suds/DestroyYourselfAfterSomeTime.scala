package improbable.behaviours.suds

import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World

import scala.concurrent.duration._

/**
  * Created by thopau on 09/04/2016.
  */
class DestroyYourselfAfterSomeTime(entity: Entity, world: World) extends EntityBehaviour {

  world.timing.after(2 seconds)
  {
    entity.destroy()
  }

}
