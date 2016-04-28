package improbable.natures

import improbable.behaviours.global.{GameStateBehaviour, GameStateSettings}
import improbable.corelib.natures.{BaseNature, NatureApplication, NatureDescription}
import improbable.corelibrary.transforms.TransformNature
import improbable.global.GameState
import improbable.math.Coordinates
import improbable.papi.entity.behaviour.EntityBehaviourDescriptor
import improbable.util.EntityPrefabs._

object GameNature extends NatureDescription {

  val globalTag = "Global"

  override def dependencies = Set[NatureDescription](BaseNature, TransformNature)

  override def activeBehaviours: Set[EntityBehaviourDescriptor] = Set(
    descriptorOf[GameStateBehaviour]
  )

  def apply(): NatureApplication = {
    val initialPosition = Coordinates(0,0,0)
    val roundLengthInSeconds = 30

    application(
      states = Seq(
        GameState(GameStateSettings.ROUND_LENGTH.get().toInt, List.empty, score = 0, minionCount = 0)
      ),
      natures = Seq(
        BaseNature(entityPrefab = GAMESTATE),
        TransformNature(initialPosition.toVector3d)
      )
    )
  }
}
