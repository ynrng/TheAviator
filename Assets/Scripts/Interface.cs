using System;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Interface : MonoBehaviour
{
    public float tmpdistance = 1; // a scaler to balance numbers
    public float tmpspeed = 1; // a scaler to balance numbers
    public float tmpenergy = 1; // a scaler to balance numbers
    private Text texts;
    StringBuilder text = new StringBuilder();
    bool needUpdate = true;
    void Awake()
    {
        Aviator.reset();//todo think where to initial
    }
    void Start()
    {
        texts = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Aviator.status) {
            case AviatorStates.Start:
                if (needUpdate) {
                    loop();// init speed and everything once
                    text.Append("CLICK TO REPLAY");
                    needUpdate = false;
                }
                break;
            case AviatorStates.Flying:
                loop();
                break;
            case AviatorStates.Falling:
                Aviator.speed *= .99f;//todo check. maybe use angular drag of sea.
                break;
        }
        texts.text = text.ToString();
    }

    void loop()
    {

        //todo uncomment to modify game
        // // Add energy coins every 100m;
        // if ((int)Aviator.distance % Aviator.distanceForCoinsSpawn == 0 && Aviator.distance > Aviator.coinLastSpawn) {
        //     Aviator.coinLastSpawn = Aviator.distance;
        //     // coinsHolder.spawnCoins();
        // }

        //todo uncomment to modify game
        // if ((int)Aviator.distance % Aviator.distanceForEnnemiesSpawn == 0 && Aviator.distance > Aviator.ennemyLastSpawn) {
        //     Aviator.ennemyLastSpawn = Aviator.distance;
        //     // ennemiesHolder.spawnEnnemies();
        // }

        updateDistance();
        updateEnergy();
        updateSpeed();

        buildString();

    }
    void updateSpeed()
    {
        if ((int)Aviator.distance % Aviator.distanceForSpeedUpdate == 0 && Aviator.distance > Aviator.speedLastUpdate) {
            Aviator.speedLastUpdate = Aviator.distance;
            Aviator.targetBaseSpeed += Aviator.incrementSpeedByTime;// * Time.deltaTime;
        }

        if ((int)Aviator.distance % Aviator.distanceForLevelUpdate == 0 && Aviator.distance > Aviator.levelLastUpdate) {
            Aviator.levelLastUpdate = Aviator.distance;
            Aviator.level++;
            Aviator.targetBaseSpeed = Aviator.initSpeed + Aviator.incrementSpeedByLevel * Aviator.level;
        }

        Aviator.planeSpeed = Aviator.normalize(Aviator.mousePos.x, -.5f, .5f, Aviator.planeMinSpeed, Aviator.planeMaxSpeed) * Time.deltaTime;
        Aviator.speed = Aviator.targetBaseSpeed * Aviator.planeSpeed;//* tmpspeed;
    }

    void buildString()
    {
        text.Clear();
        text.Append(String.Format("Level {0}\n", Aviator.level));
        text.Append(String.Format("Distance {0}\n", (int)Aviator.distance));
        text.Append(String.Format("Energy {0}\n", (int)Aviator.energy));
        needUpdate = true;
    }

    void updateEnergy()
    {
        // Time.deltaTime: at 60 FPS the value should be around 1f / 60 = 0.016f
        // so i want energy to reduce every seconds approximately
        Aviator.energy -= Aviator.speed * Aviator.ratioSpeedEnergy;//* tmpenergy;
        Aviator.energy = Mathf.Max(Aviator.energy, 0);

        // energyBar.style.right = (100 - Aviator.energy) + "%";
        // energyBar.style.backgroundColor = (Aviator.energy < 50) ? "#f25346" : "#68c3c0";

        // if (Aviator.energy < 30) {
        //     energyBar.style.animationName = "blinking";
        // } else {
        //     energyBar.style.animationName = "none";
        // }

        if (Aviator.energy < 1) {
            Aviator.status = AviatorStates.Falling;
        }
    }

    void updateDistance()
    {
        Aviator.distance += Aviator.speed * Aviator.ratioSpeedDistance;//* tmpdistance;
        //todo draw percentage
        // var d = 502 * (1 - (Aviator.distance % Aviator.distanceForLevelUpdate) / Aviator.distanceForLevelUpdate);
        // levelCircle.setAttribute("stroke-dashoffset", d);

    }

}
