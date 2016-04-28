package improbable.launch.bridgesettings


import improbable.fapi.bridge._
import improbable.fapi.network.{RakNetLinkSettings, TcpLinkSettings}
import improbable.unity.fabric.engine.EnginePlatform
import improbable.unity.fabric.engine.EnginePlatform._
import improbable.unity.fabric.satisfiers.{AggregateSatisfiers, SatisfySingleConstraint, SatisfySpecificEngine}
import improbable.unity.fabric.{AuthoritativeEntityOnly, VisualEngineConstraint}

import scala.concurrent.duration.Duration

object DemonstrationUnityClientBridgeSettings extends BridgeSettingsResolver {

  import scala.concurrent.duration._

  private val globalTag = "Global"

  private val CLIENT_ENGINE_BRIDGE_SETTINGS = BridgeSettings(
    DemonstrationClientAssetContextDiscriminator(),
    RakNetLinkSettings.apply(100 milliseconds, 5 seconds),
    EnginePlatform.UNITY_CLIENT_ENGINE,
    AggregateSatisfiers(
      SatisfySpecificEngine,
      SatisfySingleConstraint(VisualEngineConstraint)
    ),
    AuthoritativeEntityOnly(),
    ConstantEngineLoadPolicy(0.5),
    PerEntityOrderedStateUpdateQos,
    TagStreamingQueryPolicy(globalTag)
  )

  private val ANDROID_CLIENT_ENGINE_BRIDGE_SETTINGS = CLIENT_ENGINE_BRIDGE_SETTINGS.copy(enginePlatform = UNITY_ANDROID_CLIENT_ENGINE)

  private val bridgeSettings = Map[String, BridgeSettings](
    UNITY_CLIENT_ENGINE -> CLIENT_ENGINE_BRIDGE_SETTINGS,
    UNITY_ANDROID_CLIENT_ENGINE -> ANDROID_CLIENT_ENGINE_BRIDGE_SETTINGS
  )

  override def engineTypeToBridgeSettings(engineType: String, metadata: String): Option[BridgeSettings] = {
    bridgeSettings.get(engineType)
  }

}
