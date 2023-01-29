using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RTS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D square;

        private GameObjects[] squares;
        private RTS.Object[] objects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Seting the window size
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Getting all squares from json file
            using (StreamReader r = new StreamReader(@"c:\users\deep\documents\projects\rts\data.json"))
            {
                string json = r.ReadToEnd();
                var root = JsonConvert.DeserializeObject<Root>(json);
                squares = root.Squares.ToArray();
                objects = root.Objects.ToArray();
            }

            square = new Texture2D(GraphicsDevice, 1, 1);
            square.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _spriteBatch.Dispose();
            // If you are creating your texture (instead of loading it with
            // Content.Load) then you must Dispose of it
            square.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            for (int i = 0; i < squares.Length; i++)
            {
                _spriteBatch.Draw(
                    square,
                    new Rectangle(
                        squares[i].x,
                        squares[i].y,
                        squares[i].w,
                        squares[i].h
                    ),
                    squares[i].GetColour()
                );
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}