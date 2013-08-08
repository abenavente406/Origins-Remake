using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameHelperLibrary.Shapes;
using Origins_Remake.Util;
using OriginsLib.Util;

namespace Origins_Remake.States
{
    public class MainMenuState : BaseGameState
    {
        enum State
        {
            Menu, 
            NewGame, 
            LoadGame, 
            Options,
            Quit
        }

        #region Fields
        State currentState = State.Menu;

        ControlManager menuControls;
        ControlManager newGameControls;
        ControlManager loadGameControls;
        ControlManager optionsControls;
        ControlManager quitControls;

        Texture2D blackOverlay;
        GaussianBlur blurOverlay;

        Label title;
        Animation anim;

        SpriteFont debugFont;

        #endregion

        #region Initialization
        public MainMenuState(Game game, GameStateManager manager)
            : base(game, manager) { }

        protected override void LoadContent()
        {
            base.LoadContent();

            var Content = gameRef.Content;

            blackOverlay = new DrawableRectangle(GraphicsDevice,
                new Vector2(MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT), Color.White, true).Texture;

            blurOverlay = new GaussianBlur(gameRef);
            blurOverlay.ComputeKernel(7, 2.0f);


            title = new Label();
            title.SpriteFont = Content.Load<SpriteFont>("Fonts\\title");
            title.Text = "O R I G I N S";
            title.Color = Color.White;
            title.Position = new Vector2(MainGame.GAME_WIDTH / 2 - title.Width / 2, 90);
            controls.Add(title);

            Texture2D[] images = new Texture2D[8];
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = Content.Load<Texture2D>("Images\\waterfall\\water" + i.ToString());
            }

            anim = new Animation(images);

            #region Menu State
            menuControls = new ControlManager(gameRef, Content.Load<SpriteFont>("Fonts\\default"));

            var start = new LinkLabel(0) { Name = "lnklblStart", Text = "New Game" };
            var load = new LinkLabel(1) { Name = "lnklblLoad", Text = "Load Game" };
            var options = new LinkLabel(2) { Name = "lnklblOptions", Text = "Options" };
            var quit = new LinkLabel(3) { Name = "lnklblQuit", Text = "Quit Game" };

            menuControls.Add(start);
            menuControls.Add(load);
            menuControls.Add(options);
            menuControls.Add(quit);

            Vector2 startPos = new Vector2(MainGame.GAME_WIDTH / 2, MainGame.GAME_HEIGHT / 1.8f);
            foreach (Control c in menuControls)
            {
                if (c is LinkLabel)
                {
                    var l = (LinkLabel)c;
                    var offset = new Vector2(c.Width / 2, 0);
                    c.Position = startPos - offset;
                    c.Selected += new EventHandler(LinkLabel_Selected);
                    c.Effect = ControlEffect.PULSE;
                    c.Color = Color.Black;
                    l.OnMouseIn += LinkLabel_OnMouseIn;

                    startPos.Y += c.Height + 10f;
                }
            }

            menuControls.SelectControl(0);
            #endregion

            #region New Game State
            newGameControls = new ControlManager(gameRef, Content.Load<SpriteFont>("Fonts\\default"));

            Label prompt = new Label();
            prompt.Text = "Enter your name";
            prompt.Name = "lblNamePrompt";
            prompt.Position = new Vector2(MainGame.GAME_WIDTH / 2 -
                prompt.SpriteFont.MeasureString(prompt.Text).X / 2, 200);
            newGameControls.Add(prompt);

            TextBox name = new TextBox(GraphicsDevice);
            name.Name = "txtName";
            name.Position = new Vector2(prompt.Position.X + 40, prompt.Position.Y + prompt.Height + 10);
            name.BackColor = Color.Transparent;
            name.ForeColor = Color.White;
            newGameControls.Add(name);

            LinkLabel startGame = new LinkLabel(2) { Name = "lnklblNewGame", Text = "Start" };
            startGame.Position = new Vector2(prompt.Position.X, name.Position.Y + 44);
            startGame.OnMouseIn += LinkLabel_OnMouseIn;
            startGame.Selected += (o, e) => { Config.currentlyPlaying = newGameControls[1].Text;  SwitchStateWithFade(new LoadingState(gameRef, StateManager, new GameplayState(gameRef, StateManager))); };
            startGame.Effect = ControlEffect.PULSE;

            LinkLabel cancel = new LinkLabel(3) { Name = "lnklblCancel", Text = "Cancel" };
            cancel.Position = new Vector2(prompt.Position.X + prompt.Width - cancel.Width, startGame.Position.Y);
            cancel.OnMouseIn += LinkLabel_OnMouseIn;
            cancel.Selected += (o, e) => { currentState = State.Menu; };
            cancel.Effect = ControlEffect.PULSE;

            newGameControls.Add(startGame);
            newGameControls.Add(cancel);
            #endregion

            #region Load Game State

            #endregion

            #region Options Game State

            #endregion

            #region Quit State
            quitControls = new ControlManager(gameRef, Content.Load<SpriteFont>("Fonts\\default"));

            Label areYouSure1 = new Label();
            areYouSure1.Name = "lblAreYouSure?";
            areYouSure1.Text = "Are you sure you want to quit?";
            areYouSure1.Position = new Vector2(MainGame.GAME_WIDTH / 2 - areYouSure1.SpriteFont.MeasureString(
                areYouSure1.Text).X / 2, 140);
            quitControls.Add(areYouSure1);

            LinkLabel yes = new LinkLabel(1) { Name = "lnklblYes", Text = "Yes" };
            yes.Position = new Vector2(areYouSure1.Position.X + 40, areYouSure1.Position.Y + areYouSure1.Height + 50);
            yes.OnMouseIn += LinkLabel_OnMouseIn;
            yes.Selected += (o, e) => { gameRef.Exit(); };
            yes.Effect = ControlEffect.PULSE;

            LinkLabel no = new LinkLabel(2) { Name = "lnklblNo", Text = "No" };
            no.Position = new Vector2(areYouSure1.Position.X + areYouSure1.Width - no.Width - 40, yes.Position.Y);
            no.OnMouseIn += LinkLabel_OnMouseIn;
            no.Selected += (o, e) => { currentState = State.Menu; };
            no.Effect = ControlEffect.PULSE;

            quitControls.Add(yes);
            quitControls.Add(no);
            #endregion

            debugFont = Content.Load<SpriteFont>("Fonts\\version");
        }
        #endregion

        #region Events
        private void LinkLabel_Selected(object sender, EventArgs e)
        {
            LinkLabel lbl = (LinkLabel)sender;

            if (lbl.Name == "lnklblStart")
                currentState = State.NewGame;
            else if (lbl.Name == "lnklblQuit")
                currentState = State.Quit;
        }
        private void LinkLabel_OnMouseIn(object sender, EventArgs e)
        {
            var lbl = (LinkLabel)sender;

            switch (currentState)
            {
                case State.Menu:
                    menuControls.SelectControl(lbl.Index);
                    break;
                case State.NewGame:
                    newGameControls.SelectControl(lbl.Index);
                    break;
                case State.LoadGame:
                    loadGameControls.SelectControl(lbl.Index);
                    break;
                case State.Options:
                    optionsControls.SelectControl(lbl.Index);
                    break;
                case State.Quit:
                    quitControls.SelectControl(lbl.Index);
                    break;
            }
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (currentState)
            {
                case State.Menu:
                    menuControls.Update(gameTime, playerIndexInControl);
                    break;
                case State.NewGame:
                    newGameControls.Update(gameTime, playerIndexInControl);
                    break;
                case State.LoadGame:
                    break;
                case State.Options:
                    optionsControls.Update(gameTime, playerIndexInControl);
                    break;
                case State.Quit:
                    quitControls.Update(gameTime, playerIndexInControl);
                    break;
            }
        }
        #endregion

        #region Draw
        public override void Draw(GameTime gameTime)
        {
            var batch = gameRef.spriteBatch;
            batch.Begin();
            anim.Draw(batch, gameTime, new Rectangle(0, 0, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT));
#if DEBUG
            batch.DrawString(debugFont, "DEBUGGING -- " + MainGame.VERSION, new Vector2(MainGame.GAME_WIDTH - debugFont.MeasureString(
                "DEBUGGING -- " + MainGame.VERSION).X - 2, MainGame.GAME_HEIGHT - debugFont.LineSpacing - 2), Color.White);
#else
            batch.DrawString(debugFont, MainGame.VERSION, new Vector2(MainGame.GAME_WIDTH - debugFont.MeasureString(
                MainGame.VERSION).X - 2, MainGame.GAME_HEIGHT - debugFont.LineSpacing - 2), Color.White);
#endif
            batch.End();

            base.Draw(gameTime);

            batch.Begin();
            {
                menuControls.Draw(batch, gameTime);
                if (currentState != State.Menu) batch.Draw(blackOverlay, Vector2.Zero, Color.Black * 0.8f);
                switch (currentState)
                {
                    case State.NewGame:
                        newGameControls.Draw(batch, gameTime);
                        break;
                    case State.LoadGame:
                        break;
                    case State.Options:
                        optionsControls.Draw(batch, gameTime);
                        break;
                    case State.Quit:
                        quitControls.Draw(batch, gameTime);
                        break;
                }

                FadeOutRect.Draw(batch, Vector2.Zero, FadeOutColor);
            }
            batch.End();
        }
        #endregion
    }
}
