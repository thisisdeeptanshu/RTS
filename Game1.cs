﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;

namespace RTS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D square;
        private Texture2D treeTexture;
        private Texture2D grassTexture;
        private Texture2D sandTexture;
        private Texture2D waterTexture;
        private Texture2D chestTexture;
        private Texture2D playerTexture;

        private Square[] squares;
        private Object[] objects;

        private readonly int WIDTH = 1280;
        private readonly int HEIGHT = 720;
        private readonly float speed = 0.5f;

        private int changeX;
        private int changeY;

        private Dictionary<Type, Texture2D> texturesMapping = new Dictionary<Type, Texture2D>();
        private Dictionary<Object, List<Type>> chestContents = new Dictionary<Object, List<Type>>();

        private Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Seting the window size
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Getting all squares and objects from json file
            using (StreamReader r = new StreamReader(@"c:\users\deep\documents\projects\rts\map_data.json"))
            {
                string json = r.ReadToEnd();
                var root = JsonConvert.DeserializeObject<Root>(json);
                squares = root.Squares.ToArray();
                objects = root.Objects.ToArray();
            }

            // Creating and loading textures
            square = new Texture2D(GraphicsDevice, 1, 1);
            square.SetData(new[] { Color.White });
            treeTexture = Content.Load<Texture2D>("tree");
            grassTexture = Content.Load<Texture2D>("Grass");
            sandTexture = Content.Load<Texture2D>("Sand");
            waterTexture = Content.Load<Texture2D>("Water");
            chestTexture = Content.Load<Texture2D>("chest");
            playerTexture = Content.Load<Texture2D>("entity"); // Maybe consider changing the texture name to player

            // Setting the textures map
            texturesMapping.Add(Type.Tree, treeTexture);
            texturesMapping.Add(Type.Grass, grassTexture);
            texturesMapping.Add(Type.Sand, sandTexture);
            texturesMapping.Add(Type.Water, waterTexture);
            texturesMapping.Add(Type.Chest, chestTexture);
            texturesMapping.Add(Type.Player, playerTexture);

            // Creating chest collections and getting the player
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].type == Type.Chest)
                {
                    chestContents.Add(objects[i], new List<Type>());
                }
                else if (objects[i].type == Type.Player)
                {
                    player = new Player(objects[i]);
                }
            }
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

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) player.y++;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)) player.x++;
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)) player.y--;
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)) player.x--;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            // Drawing all the squares
            for (int i = 0; i < squares.Length; i++)
            {
                _spriteBatch.Draw(
                    texturesMapping[squares[i].type],
                    new Rectangle(
                        (int) ((squares[i].x - player.x) * squares[i].w + WIDTH / 2 - player.w / 2),
                        (int) ((squares[i].y - player.y) * squares[i].h + HEIGHT / 2 - player.h / 2),
                        squares[i].w,
                        squares[i].h
                    ),
                    Color.White
                );
            }
            // Drawing all the objects
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].type != Type.Player)
                {
                    _spriteBatch.Draw(
                        texturesMapping[objects[i].type],
                        new Rectangle(
                            (int)((objects[i].x - player.x) * objects[i].w + WIDTH / 2 - player.w / 2),
                            (int)((objects[i].y - player.y) * objects[i].h + HEIGHT / 2 - player.h / 2),
                            objects[i].w,
                            objects[i].h
                        ),
                        Color.White
                    );
                }
            }
            // Drawing the player
            _spriteBatch.Draw(
                texturesMapping[Type.Player],
                new Rectangle(WIDTH / 2 - player.w / 2, HEIGHT / 2 - player.h / 2, player.w, player.h),
                Color.White
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}