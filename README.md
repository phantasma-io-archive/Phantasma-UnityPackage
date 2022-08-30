# UnitySDK
Unity SDK for Phantasma

# Adding the SDK to your project
To add the SDK to your project, you need to:
* First download the [unitypackage](https://github.com/phantasma-io/UnitySDK/releases/tag/v1.1)

## Second install the unitypackage by importing it to your project
* Either double-click on the file while you have your Unity project opened
* Inside Unity, on the top left go to `Assets` -> `Import Package` -> `Custom Package...` and Select the **.unitypackage**

# How to connect to the Wallet via the SDK
Setup your scene, Add the PhantasmaLinkClient prefab to your scene.
* If you're developing in a local node, change the "Nexus" to `localnet`
* If you're deploying it to the testnet, change the "Nexus" to `testnet`
* If you're deploying it to the mainnet, change the "Nexus" to `mainnet`
* Change the DappID to your Dapp "contract name", this is what will appear when a user log's in to your Dapp.
* Recommended version is 2
* Wallet Endpoint default:`localhost:7090` (Don't change it)
* Regarding the Platform and Signature, For `Phantasma` -> `ED25519`, for `Ethereum` -> `ECDSA`

# How to connect to the wallet via Bluetooth
The same thing as the normal method, but you need to added the PhantasmaLinkClientPlugin Prefab to your Scene.

And that's it, just build for Android, you're done!

# Any further questions
- Contact Phantasma Force Team
