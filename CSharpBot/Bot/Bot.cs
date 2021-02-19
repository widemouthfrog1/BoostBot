﻿using System.Numerics;
using System.Windows.Media;
using System.Collections.Generic;
using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;

namespace Bot
{

    // We want to our bot to derive from Bot, and then implement its abstract methods.
    class Bot : RLBotDotNet.Bot
    {
        List<BoostPad> boostPads = new List<BoostPad>();
        List<Car> teamMates = new List<Car>();
        List<Car> opponents = new List<Car>();
        Car myCar;

        // We want the constructor for our Bot to extend from RLBotDotNet.Bot, but we don't want to add anything to it.
        // You might want to add logging initialisation or other types of setup up here before the bot starts.
        public Bot(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex) {
            FieldInfo fieldInfo = GetFieldInfo();
            for (int i = 0; i < fieldInfo.BoostPads.Length; i++) {
                
                boostPads.Add(
                    new BoostPad(
                        new Vector(
                            fieldInfo.BoostPads[i].Location.X, fieldInfo.BoostPads[i].Location.Y, fieldInfo.BoostPads[i].Location.Z), 
                            fieldInfo.BoostPads[i].IsFullBoost ? 100 : 12
                    )
                );
            }
        }

        public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
        {
            // We process the gameTickPacket and convert it to our own internal data structure.
            Packet packet = new Packet(gameTickPacket);

            for(int i = 0; i < packet.BoostPadStates.Length; i++)
            {
                boostPads[i].nextRespawn = packet.BoostPadStates[i].IsActive ? 0 : packet.BoostPadStates[i].Timer;
            }

            myCar = new Car(packet.Players[index]);
            for(int i = 0; i < packet.Players.Length; i++)
            {
                if(i != index)
                {
                    Car next = new Car(packet.Players[i]);
                    if (next.player.Team == myCar.player.Team)
                    {
                        teamMates.Add(next);
                    }
                    else
                    {
                        opponents.Add(next);
                    }
                }
            }

            // Get the data required to drive to the ball.
            Vector3 ballLocation = packet.Ball.Physics.Location;
            Vector3 carLocation = packet.Players[index].Physics.Location;
            Orientation carRotation = packet.Players[index].Physics.Rotation;
            

            // Find where the ball is relative to us.
            Vector3 ballRelativeLocation = Orientation.RelativeLocation(carLocation, ballLocation, carRotation);

            // Decide which way to steer in order to get to the ball.
            // If the ball is to our left, we steer left. Otherwise we steer right.
            float steer;
            if (ballRelativeLocation.Y > 0)
                steer = 1;
            else
                steer = -1;
            
            // Examples of rendering in the game
            Renderer.DrawString3D("Ball", Colors.Black, ballLocation, 3, 3);
            Renderer.DrawString3D(steer > 0 ? "Right" : "Left", Colors.Aqua, carLocation, 3, 3);
            Renderer.DrawLine3D(Colors.Red, carLocation, ballLocation);
            
            // This controller will contain all the inputs that we want the bot to perform.
            return new Controller
            {
                // Set the throttle to 1 so the bot can move.
                Throttle = 1,
                Steer = steer
            };
        }
        
        // Hide the old methods that return Flatbuffers objects and use our own methods that
        // use processed versions of those objects instead.
        internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
        internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
    }
}