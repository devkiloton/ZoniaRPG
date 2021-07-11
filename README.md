# ZoniaRPG #

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/GameScene.png)

- ZoniaRPG can be described as a micro multiplayer game where players just destroy other players, but to me was an amazing experience that involves cloud computing, Linux, C# libraries and network administration. Come check out it!

# Where can I play?

- The game isn't avaible to play because the 750 free hours of AWS EC2 instance is out :/

# Why? 

- I started this project with the intention to apply my knowledge with virtual machines and cloud computing that I learned through online courses, that was my biggest focus in this project and that's why this game is so minimalistic.

- Another reason to make this project was my curiosity about multiplayer games and how to use the Mirror library, a C# library used to develop multiplayer games.

- Any ideas or suggestions:

  Email-me: dev.kiloton@gmail.com

# How do I did it?

- First of all, I have defined a DNS where the players will connect. To do it I created an EC2 instance on AWS.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/WithoutDNS.png)

- For this project I used a t2.micro with Amazon Linux 2.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/AWSConfig.png)

- After that I created a keypair that I used to do a SSH connection.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/CreatingAndSavingKeyPair.png)

- Voilà, instance running!

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/LaunchedInstance.png)

- Now that the instance is running we need to do the SSH connection. First of all I created a private key using the Puttygen.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/MakingPrivateKeyWithKeyPair.png)

- With the private key saved, I used Putty to start the SSH connection setting up the instance user@dns.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/SettingUpUserAndDns.png)

- After that I used the private key to authenticate.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/SettingUpSSHPrivateKey.png)

- I saved the configs and accessed the instance! :happy:

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/InstaceWorking.png)

- Now since was everything ok in the instance, I have defined the DNS where the players will connect on the game.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/SettingIPMirror.png)

- Now that the game is finished I did a Linux server build that will be used in the EC2 instance. 

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/LinuxBuild.png)

- After that, a Windows client build that will be used to play the game.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/WindowsBuild.png)

- Here I used the WinSCP to upload the Linux server build. But to do that I need to do a SSH connection again.

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/SettingUpDns-UserAndSshPrivateKeyWinSCP.png)

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/UploadingFilesToTheServer.png)

- Now since everything is uploaded, I accessed the Putty and checked my directories.

  ```
  $ ls
  ```

- When I saw that the directory that I uploaded was there, I accessed it.

  ```
  $ cd build_zonia_rpg_server
  ```

- I checked my directories again.

  ```
  $ ls
  ```

- I turned the build_zonia_rpg_server.x86_64(the game) an executable file.

  ```
  $ chmod +x build_zonia_rpg_server.x86_64
  ```

- Finally I initialized the game.

  ```
  $ ./build_zonia_rpg_server.x86_64
  ```

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/Build.Exe.png)

- Now the game is running!

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/GameRunning.png)

- Since everything is ok I started the game two times and connect two players!

![alt](https://github.com/devkiloton/ZoniaRPG/blob/master/ZoniaReadmeImages/EverythingOk.png)

Voilà!

# Check the scripts

- [Click here](https://github.com/devkiloton/ZoniaRPG/tree/master/ZoniaRPG/Assets/Scripts)

# Final considerations

- Dedicated to the moments that I thought be impossible to solve connection problems with the Mirror!