open System;
open System.Windows;
open System.Windows.Forms;
open System.Drawing;
open System.Drawing.Drawing2D;

type MainWindow() = 
    inherit Form()

    let btnNumbers :Button array = Array.zeroCreate 10

    let btnAdd = new Button()
    let btnMin = new Button()
    let btnMul = new Button()
    let btnDiv = new Button()

    let btnEqual = new Button()
    let btnClear = new Button()

    let lblDisplay = new Label()

    let mutable doClear = false

    let mutable operation = 0
    let mutable operand1 :Int64 = int64 0
    let mutable operand2 :Int64 = int64 0

    member this.Init =
        this.Text <- "Prosty kalkulator w F#"
        this.Size <- new Size(260, 300)
        this.FormBorderStyle <- FormBorderStyle.Fixed3D
        this.MaximizeBox <- false
        this.BackColor <- Color.AliceBlue

        lblDisplay.Location <- Point(30, 10)
        lblDisplay.Size <- Size(190, 40)
        lblDisplay.Text <- "0"
        lblDisplay.BackColor <- Color.White
        lblDisplay.Font <- new Font("Arial", 16.0f)
        lblDisplay.BorderStyle <- BorderStyle.Fixed3D
        lblDisplay.TextAlign <- ContentAlignment.BottomRight

        let mutable btnX = 30
        let mutable btnY = 210

        for i in 0 .. btnNumbers.Length - 1 do

            btnNumbers.[i] <- new Button()

            btnNumbers.[i].Location <- Point(btnX, btnY)
            btnNumbers.[i].Size <- Size(40, 40)
            btnNumbers.[i].Text <- i.ToString()
            btnNumbers.[i].Click.AddHandler(new EventHandler(fun s e -> this.Event_Number_Pressed(s, e)))

            this.Controls.Add(btnNumbers.[i])

            if i = 0 then
                btnY <- btnY - 50
            else
                if i % 3 = 0 then
                    btnX <- 30
                    btnY <- btnY - 50
                else
                    btnX <- btnX + 50

        btnNumbers.[0].BackColor <- Color.FromArgb(146, 216, 250)
        btnNumbers.[1].BackColor <- btnNumbers.[0].BackColor
        btnNumbers.[2].BackColor <- btnNumbers.[1].BackColor
        btnNumbers.[3].BackColor <- btnNumbers.[2].BackColor

        btnNumbers.[4].BackColor <- btnNumbers.[3].BackColor
        btnNumbers.[5].BackColor <- btnNumbers.[4].BackColor
        btnNumbers.[6].BackColor <- btnNumbers.[5].BackColor

        btnNumbers.[7].BackColor <- btnNumbers.[6].BackColor
        btnNumbers.[8].BackColor <- btnNumbers.[7].BackColor
        btnNumbers.[9].BackColor <- btnNumbers.[8].BackColor

        btnAdd.Location <- Point(180, 210)
        btnAdd.Size <- Size(40, 40)
        btnAdd.Text <- "+"
        btnAdd.BackColor <- Color.FromArgb(187, 208, 185)
        btnAdd.Click.AddHandler(new EventHandler(fun s e -> this.Event_Operation_Pressed(s, e)))

        btnMin.Location <- Point(180, 160)
        btnMin.Size <- Size(40, 40)
        btnMin.Text <- "-"
        btnMin.BackColor <- btnAdd.BackColor
        btnMin.Click.AddHandler(new EventHandler(fun s e -> this.Event_Operation_Pressed(s, e)))

        btnMul.Location <- Point(180, 110)
        btnMul.Size <- Size(40, 40)
        btnMul.Text <- "*"
        btnMul.BackColor <- btnAdd.BackColor
        btnMul.Click.AddHandler(new EventHandler(fun s e -> this.Event_Operation_Pressed(s, e)))

        btnDiv.Location <- Point(180, 60)
        btnDiv.Size <- Size(40, 40)
        btnDiv.Text <- "/"
        btnDiv.BackColor <- btnAdd.BackColor
        btnDiv.Click.AddHandler(new EventHandler(fun s e -> this.Event_Operation_Pressed(s, e)))

        btnClear.Location <- Point(80, 210)
        btnClear.Size <- Size(40, 40)
        btnClear.Text <- "C"
        btnClear.Font <- new Font("Arial", 14.0f)
        btnClear.ForeColor <- Color.White
        btnClear.BackColor <- Color.Red
        btnClear.Click.AddHandler(new EventHandler(fun s e -> this.Event_Clear_Pressed(s, e)))

        btnEqual.Location <- Point(130, 210)
        btnEqual.Size <- Size(40, 40)
        btnEqual.Text <- "="
        btnEqual.Font <- new Font("Arial", 16.0f)
        btnEqual.ForeColor <- Color.White
        btnEqual.BackColor <- Color.Gray

        btnEqual.Click.AddHandler(new EventHandler(fun s e -> this.Event_Equal_Pressed(s, e)))

        this.Controls.Add(lblDisplay)

        this.Controls.Add(btnClear)
        this.Controls.Add(btnEqual)
        this.Controls.Add(btnAdd)
        this.Controls.Add(btnMin)
        this.Controls.Add(btnMul)
        this.Controls.Add(btnDiv)

    member this.DoOperation() = 
        if operation = 1 then 
            lblDisplay.Text <- (operand1 + operand2).ToString()
        else if operation = 2 then 
            lblDisplay.Text <- (operand1 - operand2).ToString()
        else if operation = 3 then 
            lblDisplay.Text <- (operand1 * operand2).ToString()
        else if operation = 4 then 
            lblDisplay.Text <- (operand1 / operand2).ToString()

    member this.Event_Clear_Pressed(sender : Object, e : EventArgs) = 
        operation <- 0
        operand1 <- int64 0
        operand2 <- int64 0

        lblDisplay.Text <- "0"

    member this.Event_Equal_Pressed(sender : Object, e : EventArgs) = 

        this.DoOperation()

        operand1 <- Int64.Parse(lblDisplay.Text)
        doClear <- true

    member this.downcastButton(b1 : Object) =
       match b1 with
       | :? Button as derived1 -> derived1
       | _ -> null

    member this.Event_Operation_Pressed(sender : Object, e : EventArgs) = 

        let btn = this.downcastButton(sender)

        operation <- 
            match btn.Text.[0] with 
            | '+' -> 1
            | '-' -> 2
            | '*' -> 3
            | '/' -> 4
            | _ -> 1
        doClear <- true

        if operand2 <> int64 0 then
            this.DoOperation()
            operand1 <- Int64.Parse(lblDisplay.Text)

    member this.Event_Number_Pressed(sender : Object, e : EventArgs) = 
        let btn = this.downcastButton(sender)

        if lblDisplay.Text = "0" then
            doClear <- true

        if doClear = true then
            lblDisplay.Text <- ""
            doClear <- false

        if lblDisplay.Text.Length < 17 then
            if operation = 0 then
                lblDisplay.Text <- lblDisplay.Text + btn.Text
                operand1 <- Int64.Parse(lblDisplay.Text)
            else
                lblDisplay.Text <- lblDisplay.Text + btn.Text
                operand2 <- Int64.Parse(lblDisplay.Text)


  

[<STAThread>]
let START = 

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    let mainWindow = new MainWindow()

    mainWindow.Init

    Application.Run(mainWindow)
