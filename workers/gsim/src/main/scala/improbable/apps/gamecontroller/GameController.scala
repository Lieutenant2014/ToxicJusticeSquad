package improbable.apps.gamecontroller

import improbable.natures.GameNature
import improbable.papi.world.AppWorld
import improbable.papi.worldapp.WorldApp

class GameController(world: AppWorld) extends WorldApp {
  Notifier.sendNotification(world, "Hello", "World")

  // Create the global game state
  var gameController = world.entities.spawnEntity(GameNature())

  world.messaging.onReceive {
    case msg: MinionReachedEvacuation =>
      world.messaging.sendToEntity(gameController, msg)
  }
}


