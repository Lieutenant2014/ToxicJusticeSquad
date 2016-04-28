package improbable.behaviours.player

import improbable.natures.{CrateNature, SpawnerNature}
import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.player.controls.PlacementState

/**
  * Created by thopau on 02/04/2016.
  */
class PlaceObjectBehaviour(entity: Entity, world: World) extends EntityBehaviour {

  entity.watch[PlacementState].onPlaceObjectEvent{
    evt =>
      {
        evt.objectId match {
          case 1 => world.entities.spawnEntity(CrateNature(evt.location))
        }
      }
  }

}
