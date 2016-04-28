package improbable.apps

import improbable.math.{Vector3d, Coordinates}

import scala.util.Random

object CityBlockUtils {
  val MIN_BLOCK = 0
  val MAX_BLOCK = 4
  def blockToPosition(x: Integer, y: Integer): Coordinates = {
    assert(x >= MIN_BLOCK)
    assert(x <= MAX_BLOCK)
    assert(y >= MIN_BLOCK)
    assert(y <= MAX_BLOCK)
    (Vector3d(90, 0, 90) - Vector3d(60, 0, 0) * x.toDouble - Vector3d(0, 0, 60) * y.toDouble).toCoordinates
  }
  def randomBlockCenterPosition(): Coordinates = {
    val x = Random.nextInt(MAX_BLOCK + 1)
    val y = Random.nextInt(MAX_BLOCK + 1)
    blockToPosition(x, y)
  }
  def randomBlockPosition(height: Double = 0.0): Coordinates = {
    val core = randomBlockCenterPosition().toVector3d
    val jitter = Vector3d(15.0f * (Random.nextDouble() - 0.5f), height, 15.0f * (Random.nextDouble() - 0.5f))
    (core + jitter).toCoordinates
  }
}
