﻿using System;
using System.Collections.Generic;
using System.Text;
using Carmageddon.HUD;
using PlatformEngine;
using Microsoft.Xna.Framework.Graphics;
using NFSEngine;
using Microsoft.Xna.Framework;
using Carmageddon.Physics;

namespace Carmageddon.CameraViews
{
    class ChaseView : ICameraView
    {
        List<BaseHUDItem> _hudItems = new List<BaseHUDItem>();
        Vehicle _vehicle;
        FixedChaseCamera _camera;

        public ChaseView(Vehicle vehicle)
        {
            _camera = new FixedChaseCamera(6.3f, 2.3f);
            _camera.FieldOfView = MathHelper.ToRadians(55.55f);

            _vehicle = vehicle;

            _hudItems.Add(new StandardHudItem());
            _hudItems.Add(new RevCounter(_vehicle.Chassis));
            _hudItems.Add(new Timer());
            
        }

        #region ICameraView Members


        public bool Selectable
        {
            get { return true; }
        }

        public void Update()
        {
            VehicleChassis chassis = _vehicle.Chassis;
            _camera.Position = _vehicle.GetBodyBottom();
            
            if (!chassis.InAir)
            {
                _camera.Orientation = chassis.Actor.GlobalOrientation.Forward;
                if (chassis.Speed > 15)
                {
                    _camera.Rotation = (chassis.Backwards ? MathHelper.Pi : 0);
                }
                if (Race.Current.RaceTime.IsStarted) _camera.HeightOverride = 0;
            }
            else
            {
                if (Race.Current.RaceTime.IsStarted) _camera.HeightOverride = 2;
            }

            foreach (BaseHUDItem item in _hudItems)
                item.Update();

            Engine.Camera = _camera;
        }

        public void Render()
        {
            _vehicle.Render();

            foreach (BaseHUDItem item in _hudItems)
                item.Render();            
        }

        public void Activate()
        {
            Engine.Camera = _camera;
        }

        public void Deactivate()
        {
            
        }

        #endregion
    }
}
