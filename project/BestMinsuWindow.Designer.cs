namespace BestMinsu_WindowApp
{
	partial class BestMinsuWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		// 공용 TabControl 및 TabPage
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;

		// 탭1 컨트롤 (웹 크롤링 및 json 저장)
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button1;           // 분석 시작
		private System.Windows.Forms.RadioButton radioButton1; // 멀티 스레드 사용 (추천)
		private System.Windows.Forms.RadioButton radioButton2; // 싱글 스레드로 실행
		private System.Windows.Forms.Button button2;           // 분석 중단

		// 탭2 컨트롤 (로컬 데이터 비교)
		private System.Windows.Forms.RichTextBox richTextBox2;
		private System.Windows.Forms.Button button3;           // 분석 시작
		private System.Windows.Forms.Button button4;           // 결과 저장
		private System.Windows.Forms.Button buttonSelectFile1; // 비교 대상 파일1 선택
		private System.Windows.Forms.Button buttonSelectFile2; // 비교 대상 파일2 선택

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Designer 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();

			// TabControl과 TabPage 초기화
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();

			// 탭1 컨트롤 초기화
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.button2 = new System.Windows.Forms.Button();

			// 탭2 컨트롤 초기화
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.buttonSelectFile1 = new System.Windows.Forms.Button();
			this.buttonSelectFile2 = new System.Windows.Forms.Button();

			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1112, 720);
			this.tabControl.TabIndex = 0;

			// 
			// tabPage1 (웹 크롤링 및 json 저장)
			// 
			this.tabPage1.Controls.Add(this.richTextBox1);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.radioButton1);
			this.tabPage1.Controls.Add(this.radioButton2);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1104, 694);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "데이터 분석";
			this.tabPage1.UseVisualStyleBackColor = true;

			// 
			// tabPage2 (로컬 데이터 비교)
			// 
			this.tabPage2.Controls.Add(this.richTextBox2);
			this.tabPage2.Controls.Add(this.button3);
			this.tabPage2.Controls.Add(this.button4);
			this.tabPage2.Controls.Add(this.buttonSelectFile1);
			this.tabPage2.Controls.Add(this.buttonSelectFile2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1104, 694);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "데이터 비교";
			this.tabPage2.UseVisualStyleBackColor = true;

			// 
			// 탭1: richTextBox1
			// 
			this.richTextBox1.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.richTextBox1.Location = new System.Drawing.Point(35, 50);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(740, 596);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// 탭1: button1 ("분석 시작")
			// 
			this.button1.Font = new System.Drawing.Font("메이플스토리", 12F);
			this.button1.Location = new System.Drawing.Point(818, 517);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(185, 53);
			this.button1.TabIndex = 1;
			this.button1.Text = "분석 시작";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// 탭1: radioButton1 ("멀티 스레드 사용 (추천)")
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.radioButton1.Location = new System.Drawing.Point(818, 60);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(174, 36);
			this.radioButton1.TabIndex = 2;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "멀티 스레드 사용 (추천)";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// 탭1: radioButton2 ("싱글 스레드로 실행")
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.radioButton2.Location = new System.Drawing.Point(818, 105);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(253, 36);
			this.radioButton2.TabIndex = 3;
			this.radioButton2.Text = "싱글 스레드로 실행";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// 탭1: button2 ("분석 중단")
			// 
			this.button2.Enabled = false;
			this.button2.Font = new System.Drawing.Font("메이플스토리", 12F);
			this.button2.Location = new System.Drawing.Point(818, 593);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(185, 53);
			this.button2.TabIndex = 4;
			this.button2.Text = "분석 중단";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);

			// 
			// 탭2: richTextBox2 (탭1과 같은 위치)
			// 
			this.richTextBox2.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.richTextBox2.Location = new System.Drawing.Point(35, 50);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ReadOnly = true;
			this.richTextBox2.Size = new System.Drawing.Size(740, 596);
			this.richTextBox2.TabIndex = 0;
			this.richTextBox2.Text = "";
			// 
			// 탭2: button3 ("분석 시작")
			// 
			this.button3.Font = new System.Drawing.Font("메이플스토리", 12F);
			this.button3.Location = new System.Drawing.Point(818, 517);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(185, 53);
			this.button3.TabIndex = 1;
			this.button3.Text = "분석 시작";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// 탭2: button4 ("결과 저장")
			// 
			this.button4.Font = new System.Drawing.Font("메이플스토리", 12F);
			this.button4.Location = new System.Drawing.Point(818, 593);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(185, 53);
			this.button4.TabIndex = 2;
			this.button4.Text = "결과 저장";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// 탭2: buttonSelectFile1 ("파일1 선택")
			// 
			this.buttonSelectFile1.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.buttonSelectFile1.Location = new System.Drawing.Point(818, 60);
			this.buttonSelectFile1.Name = "buttonSelectFile1";
			this.buttonSelectFile1.Size = new System.Drawing.Size(253, 36);
			this.buttonSelectFile1.TabIndex = 3;
			this.buttonSelectFile1.Text = "파일1 선택";
			this.buttonSelectFile1.UseVisualStyleBackColor = true;
			this.buttonSelectFile1.Click += new System.EventHandler(this.buttonSelectFile1_Click);
			// 
			// 탭2: buttonSelectFile2 ("파일2 선택")
			// 
			this.buttonSelectFile2.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.buttonSelectFile2.Location = new System.Drawing.Point(818, 105);
			this.buttonSelectFile2.Name = "buttonSelectFile2";
			this.buttonSelectFile2.Size = new System.Drawing.Size(253, 36);
			this.buttonSelectFile2.TabIndex = 4;
			this.buttonSelectFile2.Text = "파일2 선택";
			this.buttonSelectFile2.UseVisualStyleBackColor = true;
			this.buttonSelectFile2.Click += new System.EventHandler(this.buttonSelectFile2_Click);

			// 
			// BestMinsuWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1112, 720);
			this.Controls.Add(this.tabControl);
			this.Name = "BestMinsuWindow";
			this.Text = "BestMinsu";
			this.ResumeLayout(false);
		}

		#endregion
	}
}
