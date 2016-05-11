using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using SecondGame.Model;

namespace SecondGame.Controller
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class SecondGame : Game
	{
		// Keyboard states used to determine key presses
		KeyboardState currentKeyboardState;
		KeyboardState previousKeyboardState;

		// Gamepad states used to determine button presses
		GamePadState currentGamePadState;
		GamePadState previousGamePadState; 

		// A movement speed for the player
		float playerMoveSpeed;
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Player player;

		public SecondGame ()
		{
			
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		}


		protected override void Initialize ()
		{
			player = new Player();

			playerMoveSpeed = 8.0f;
            
			base.Initialize ();
		}


		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			// Load the player resources 
			Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,GraphicsDevice.Viewport.TitleSafeArea.Y +GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
			player.Initialize(Content.Load<Texture2D>("Textures/player"), playerPosition);
			//TODO: use this.Content to load your game content here 
		}


		#region update Region
		private void UpdatePlayer(GameTime gameTime)
		{

			// Get Thumbstick Controls
			player.Position.X += currentGamePadState.ThumbSticks.Left.X *playerMoveSpeed;
			player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y *playerMoveSpeed;

			// Use the Keyboard / Dpad
			if (currentKeyboardState.IsKeyDown(Keys.Left) ||
				currentGamePadState.DPad.Left == ButtonState.Pressed)
			{
				player.Position.X -= playerMoveSpeed;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Right) ||
				currentGamePadState.DPad.Right == ButtonState.Pressed)
			{
				player.Position.X += playerMoveSpeed;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Up) ||
				currentGamePadState.DPad.Up == ButtonState.Pressed)
			{
				player.Position.Y -= playerMoveSpeed;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Down) ||
				currentGamePadState.DPad.Down == ButtonState.Pressed)
			{
				player.Position.Y += playerMoveSpeed;
			}
			#endregion
			// Make sure that the player does not go out of bounds
			player.Position.X = MathHelper.Clamp(player.Position.X, 0,GraphicsDevice.Viewport.Width - player.Width);
			player.Position.Y = MathHelper.Clamp(player.Position.Y, 0,GraphicsDevice.Viewport.Height - player.Height);
		}
		protected override void Update (GameTime gameTime)
		{
			


			//Update the player

			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
            
			// TODO: Add your update logic here
			previousGamePadState = currentGamePadState;
			previousKeyboardState = currentKeyboardState;

			// Read the current state of the keyboard and gamepad and store it
			currentKeyboardState = Keyboard.GetState();
			currentGamePadState = GamePad.GetState(PlayerIndex.One);


			UpdatePlayer(gameTime);

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.DeepPink);
            
			//TODO: Add your drawing code here
			// Start drawing
			spriteBatch.Begin();

			// Draw the Player
			player.Draw(spriteBatch);

			// Stop drawing
			spriteBatch.End();

			base.Draw (gameTime);
		}
	}
}

