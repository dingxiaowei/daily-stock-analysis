namespace StockDatasCollection.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabStockMgr = new System.Windows.Forms.TabPage();
            this.dgvStocks = new System.Windows.Forms.DataGridView();
            this.colIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelStockButtons = new System.Windows.Forms.Panel();
            this.btnAddStock = new System.Windows.Forms.Button();
            this.btnEditStock = new System.Windows.Forms.Button();
            this.btnDeleteStock = new System.Windows.Forms.Button();
            this.tabCollect = new System.Windows.Forms.TabPage();
            this.dgvLiveData = new System.Windows.Forms.DataGridView();
            this.colLiveCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLivePreClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveHigh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveLow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveVol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveTurnover = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelCollectSettings = new System.Windows.Forms.Panel();
            this.lblIntervalCollect = new System.Windows.Forms.Label();
            this.nudCollectInterval = new System.Windows.Forms.NumericUpDown();
            this.lblSecCollect = new System.Windows.Forms.Label();
            this.btnStartCollect = new System.Windows.Forms.Button();
            this.btnStopCollect = new System.Windows.Forms.Button();
            this.lblCollectStatus = new System.Windows.Forms.Label();
            this.lblLastCollect = new System.Windows.Forms.Label();
            this.lblNextCollect = new System.Windows.Forms.Label();
            this.lblCacheCount = new System.Windows.Forms.Label();
            this.chkTimeRestrict = new System.Windows.Forms.CheckBox();
            this.tabArchive = new System.Windows.Forms.TabPage();
            this.rtbArchiveLog = new System.Windows.Forms.RichTextBox();
            this.panelArchiveSettings = new System.Windows.Forms.Panel();
            this.lblIntervalArchive = new System.Windows.Forms.Label();
            this.nudArchiveInterval = new System.Windows.Forms.NumericUpDown();
            this.lblSecArchive = new System.Windows.Forms.Label();
            this.lblArchiveDir = new System.Windows.Forms.Label();
            this.txtArchiveDir = new System.Windows.Forms.TextBox();
            this.btnBrowseArchiveDir = new System.Windows.Forms.Button();
            this.btnStartArchive = new System.Windows.Forms.Button();
            this.btnStopArchive = new System.Windows.Forms.Button();
            this.btnArchiveNow = new System.Windows.Forms.Button();
            this.lblLastArchive = new System.Windows.Forms.Label();
            this.lblNextArchive = new System.Windows.Forms.Label();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.webBrowserChart = new System.Windows.Forms.WebBrowser();
            this.panelChartTop = new System.Windows.Forms.Panel();
            this.btnRefreshChart = new System.Windows.Forms.Button();
            this.lblChartHint = new System.Windows.Forms.Label();
            this.tabLoader = new System.Windows.Forms.TabPage();
            this.dgvLoadData = new System.Windows.Forms.DataGridView();
            this.colLdCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdCollectedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdPreClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdHigh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdLow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdVol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdTurnover = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdBuy1Vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdBuy1Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdSell1Vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdSell1Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLdStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLoadTop = new System.Windows.Forms.Panel();
            this.txtLoadPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.btnBrowseDir = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblLoadStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabStockMgr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocks)).BeginInit();
            this.panelStockButtons.SuspendLayout();
            this.tabCollect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiveData)).BeginInit();
            this.panelCollectSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCollectInterval)).BeginInit();
            this.tabArchive.SuspendLayout();
            this.panelArchiveSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArchiveInterval)).BeginInit();
            this.tabChart.SuspendLayout();
            this.panelChartTop.SuspendLayout();
            this.tabLoader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadData)).BeginInit();
            this.panelLoadTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabStockMgr);
            this.tabControl.Controls.Add(this.tabCollect);
            this.tabControl.Controls.Add(this.tabArchive);
            this.tabControl.Controls.Add(this.tabLoader);
            this.tabControl.Controls.Add(this.tabChart);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 650);
            this.tabControl.TabIndex = 0;
            // 
            // tabStockMgr
            // 
            this.tabStockMgr.Controls.Add(this.dgvStocks);
            this.tabStockMgr.Controls.Add(this.panelStockButtons);
            this.tabStockMgr.Location = new System.Drawing.Point(4, 26);
            this.tabStockMgr.Name = "tabStockMgr";
            this.tabStockMgr.Padding = new System.Windows.Forms.Padding(5);
            this.tabStockMgr.Size = new System.Drawing.Size(992, 620);
            this.tabStockMgr.TabIndex = 0;
            this.tabStockMgr.Text = "股票管理";
            // 
            // dgvStocks
            // 
            this.dgvStocks.AllowUserToAddRows = false;
            this.dgvStocks.AllowUserToDeleteRows = false;
            this.dgvStocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdx,
            this.colCode,
            this.colName,
            this.colNotes});
            this.dgvStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStocks.Location = new System.Drawing.Point(5, 5);
            this.dgvStocks.Name = "dgvStocks";
            this.dgvStocks.ReadOnly = true;
            this.dgvStocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStocks.Size = new System.Drawing.Size(982, 565);
            this.dgvStocks.TabIndex = 0;
            // 
            // colIdx
            // 
            this.colIdx.FillWeight = 30F;
            this.colIdx.HeaderText = "序号";
            this.colIdx.Name = "colIdx";
            this.colIdx.ReadOnly = true;
            // 
            // colCode
            // 
            this.colCode.FillWeight = 60F;
            this.colCode.HeaderText = "代码";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.FillWeight = 80F;
            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colNotes
            // 
            this.colNotes.FillWeight = 130F;
            this.colNotes.HeaderText = "备注";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // panelStockButtons
            // 
            this.panelStockButtons.Controls.Add(this.btnAddStock);
            this.panelStockButtons.Controls.Add(this.btnEditStock);
            this.panelStockButtons.Controls.Add(this.btnDeleteStock);
            this.panelStockButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStockButtons.Location = new System.Drawing.Point(5, 570);
            this.panelStockButtons.Name = "panelStockButtons";
            this.panelStockButtons.Size = new System.Drawing.Size(982, 45);
            this.panelStockButtons.TabIndex = 1;
            // 
            // btnAddStock
            // 
            this.btnAddStock.Location = new System.Drawing.Point(5, 7);
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(80, 30);
            this.btnAddStock.TabIndex = 0;
            this.btnAddStock.Text = "添加";
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // btnEditStock
            // 
            this.btnEditStock.Location = new System.Drawing.Point(95, 7);
            this.btnEditStock.Name = "btnEditStock";
            this.btnEditStock.Size = new System.Drawing.Size(80, 30);
            this.btnEditStock.TabIndex = 1;
            this.btnEditStock.Text = "编辑";
            this.btnEditStock.Click += new System.EventHandler(this.btnEditStock_Click);
            // 
            // btnDeleteStock
            // 
            this.btnDeleteStock.Location = new System.Drawing.Point(185, 7);
            this.btnDeleteStock.Name = "btnDeleteStock";
            this.btnDeleteStock.Size = new System.Drawing.Size(80, 30);
            this.btnDeleteStock.TabIndex = 2;
            this.btnDeleteStock.Text = "删除";
            this.btnDeleteStock.Click += new System.EventHandler(this.btnDeleteStock_Click);
            // 
            // tabCollect
            // 
            this.tabCollect.Controls.Add(this.dgvLiveData);
            this.tabCollect.Controls.Add(this.panelCollectSettings);
            this.tabCollect.Location = new System.Drawing.Point(4, 26);
            this.tabCollect.Name = "tabCollect";
            this.tabCollect.Padding = new System.Windows.Forms.Padding(5);
            this.tabCollect.Size = new System.Drawing.Size(992, 620);
            this.tabCollect.TabIndex = 1;
            this.tabCollect.Text = "数据采集";
            // 
            // dgvLiveData
            // 
            this.dgvLiveData.AllowUserToAddRows = false;
            this.dgvLiveData.AllowUserToDeleteRows = false;
            this.dgvLiveData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLiveData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLiveCode,
            this.colLiveName,
            this.colLiveCurrent,
            this.colLiveOpen,
            this.colLivePreClose,
            this.colLiveHigh,
            this.colLiveLow,
            this.colLiveVol,
            this.colLiveTurnover,
            this.colLiveDate,
            this.colLiveTime});
            this.dgvLiveData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLiveData.Location = new System.Drawing.Point(5, 120);
            this.dgvLiveData.Name = "dgvLiveData";
            this.dgvLiveData.ReadOnly = true;
            this.dgvLiveData.Size = new System.Drawing.Size(982, 495);
            this.dgvLiveData.TabIndex = 0;
            // 
            // colLiveCode
            // 
            this.colLiveCode.HeaderText = "代码";
            this.colLiveCode.Name = "colLiveCode";
            this.colLiveCode.ReadOnly = true;
            // 
            // colLiveName
            // 
            this.colLiveName.HeaderText = "名称";
            this.colLiveName.Name = "colLiveName";
            this.colLiveName.ReadOnly = true;
            // 
            // colLiveCurrent
            // 
            this.colLiveCurrent.HeaderText = "现价";
            this.colLiveCurrent.Name = "colLiveCurrent";
            this.colLiveCurrent.ReadOnly = true;
            // 
            // colLiveOpen
            // 
            this.colLiveOpen.HeaderText = "今开";
            this.colLiveOpen.Name = "colLiveOpen";
            this.colLiveOpen.ReadOnly = true;
            // 
            // colLivePreClose
            // 
            this.colLivePreClose.HeaderText = "昨收";
            this.colLivePreClose.Name = "colLivePreClose";
            this.colLivePreClose.ReadOnly = true;
            // 
            // colLiveHigh
            // 
            this.colLiveHigh.HeaderText = "最高";
            this.colLiveHigh.Name = "colLiveHigh";
            this.colLiveHigh.ReadOnly = true;
            // 
            // colLiveLow
            // 
            this.colLiveLow.HeaderText = "最低";
            this.colLiveLow.Name = "colLiveLow";
            this.colLiveLow.ReadOnly = true;
            // 
            // colLiveVol
            // 
            this.colLiveVol.HeaderText = "成交量";
            this.colLiveVol.Name = "colLiveVol";
            this.colLiveVol.ReadOnly = true;
            // 
            // colLiveTurnover
            // 
            this.colLiveTurnover.HeaderText = "成交额";
            this.colLiveTurnover.Name = "colLiveTurnover";
            this.colLiveTurnover.ReadOnly = true;
            // 
            // colLiveDate
            // 
            this.colLiveDate.HeaderText = "交易日期";
            this.colLiveDate.Name = "colLiveDate";
            this.colLiveDate.ReadOnly = true;
            // 
            // colLiveTime
            // 
            this.colLiveTime.HeaderText = "交易时间";
            this.colLiveTime.Name = "colLiveTime";
            this.colLiveTime.ReadOnly = true;
            // 
            // panelCollectSettings
            // 
            this.panelCollectSettings.Controls.Add(this.lblIntervalCollect);
            this.panelCollectSettings.Controls.Add(this.nudCollectInterval);
            this.panelCollectSettings.Controls.Add(this.lblSecCollect);
            this.panelCollectSettings.Controls.Add(this.btnStartCollect);
            this.panelCollectSettings.Controls.Add(this.btnStopCollect);
            this.panelCollectSettings.Controls.Add(this.lblCollectStatus);
            this.panelCollectSettings.Controls.Add(this.lblLastCollect);
            this.panelCollectSettings.Controls.Add(this.lblNextCollect);
            this.panelCollectSettings.Controls.Add(this.lblCacheCount);
            this.panelCollectSettings.Controls.Add(this.chkTimeRestrict);
            this.panelCollectSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCollectSettings.Location = new System.Drawing.Point(5, 5);
            this.panelCollectSettings.Name = "panelCollectSettings";
            this.panelCollectSettings.Size = new System.Drawing.Size(982, 115);
            this.panelCollectSettings.TabIndex = 1;
            // 
            // lblIntervalCollect
            // 
            this.lblIntervalCollect.AutoSize = true;
            this.lblIntervalCollect.Location = new System.Drawing.Point(10, 12);
            this.lblIntervalCollect.Name = "lblIntervalCollect";
            this.lblIntervalCollect.Size = new System.Drawing.Size(79, 17);
            this.lblIntervalCollect.TabIndex = 0;
            this.lblIntervalCollect.Text = "采集间隔(秒):";
            // 
            // nudCollectInterval
            // 
            this.nudCollectInterval.Location = new System.Drawing.Point(110, 9);
            this.nudCollectInterval.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.nudCollectInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCollectInterval.Name = "nudCollectInterval";
            this.nudCollectInterval.Size = new System.Drawing.Size(70, 23);
            this.nudCollectInterval.TabIndex = 1;
            this.nudCollectInterval.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblSecCollect
            // 
            this.lblSecCollect.AutoSize = true;
            this.lblSecCollect.Location = new System.Drawing.Point(185, 12);
            this.lblSecCollect.Name = "lblSecCollect";
            this.lblSecCollect.Size = new System.Drawing.Size(20, 17);
            this.lblSecCollect.TabIndex = 2;
            this.lblSecCollect.Text = "秒";
            // 
            // btnStartCollect
            // 
            this.btnStartCollect.Location = new System.Drawing.Point(10, 40);
            this.btnStartCollect.Name = "btnStartCollect";
            this.btnStartCollect.Size = new System.Drawing.Size(90, 30);
            this.btnStartCollect.TabIndex = 3;
            this.btnStartCollect.Text = "开始采集";
            this.btnStartCollect.Click += new System.EventHandler(this.btnStartCollect_Click);
            // 
            // btnStopCollect
            // 
            this.btnStopCollect.Enabled = false;
            this.btnStopCollect.Location = new System.Drawing.Point(110, 40);
            this.btnStopCollect.Name = "btnStopCollect";
            this.btnStopCollect.Size = new System.Drawing.Size(90, 30);
            this.btnStopCollect.TabIndex = 4;
            this.btnStopCollect.Text = "停止采集";
            this.btnStopCollect.Click += new System.EventHandler(this.btnStopCollect_Click);
            // 
            // lblCollectStatus
            // 
            this.lblCollectStatus.AutoSize = true;
            this.lblCollectStatus.Location = new System.Drawing.Point(10, 80);
            this.lblCollectStatus.Name = "lblCollectStatus";
            this.lblCollectStatus.Size = new System.Drawing.Size(80, 17);
            this.lblCollectStatus.TabIndex = 5;
            this.lblCollectStatus.Text = "状态：已停止";
            // 
            // lblLastCollect
            // 
            this.lblLastCollect.AutoSize = true;
            this.lblLastCollect.Location = new System.Drawing.Point(130, 80);
            this.lblLastCollect.Name = "lblLastCollect";
            this.lblLastCollect.Size = new System.Drawing.Size(81, 17);
            this.lblLastCollect.TabIndex = 6;
            this.lblLastCollect.Text = "最后采集：—";
            // 
            // lblNextCollect
            // 
            this.lblNextCollect.AutoSize = true;
            this.lblNextCollect.Location = new System.Drawing.Point(300, 80);
            this.lblNextCollect.Name = "lblNextCollect";
            this.lblNextCollect.Size = new System.Drawing.Size(81, 17);
            this.lblNextCollect.TabIndex = 7;
            this.lblNextCollect.Text = "下次采集：—";
            // 
            // lblCacheCount
            // 
            this.lblCacheCount.AutoSize = true;
            this.lblCacheCount.Location = new System.Drawing.Point(470, 80);
            this.lblCacheCount.Name = "lblCacheCount";
            this.lblCacheCount.Size = new System.Drawing.Size(75, 17);
            this.lblCacheCount.TabIndex = 8;
            this.lblCacheCount.Text = "缓存记录：0";
            // 
            // chkTimeRestrict
            // 
            this.chkTimeRestrict.AutoSize = true;
            this.chkTimeRestrict.Checked = true;
            this.chkTimeRestrict.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeRestrict.Location = new System.Drawing.Point(220, 11);
            this.chkTimeRestrict.Name = "chkTimeRestrict";
            this.chkTimeRestrict.Size = new System.Drawing.Size(170, 21);
            this.chkTimeRestrict.TabIndex = 9;
            this.chkTimeRestrict.Text = "仅开盘时段采集（关闭可测试）";
            this.chkTimeRestrict.UseVisualStyleBackColor = true;
            // 
            // tabArchive
            // 
            this.tabArchive.Controls.Add(this.rtbArchiveLog);
            this.tabArchive.Controls.Add(this.panelArchiveSettings);
            this.tabArchive.Location = new System.Drawing.Point(4, 26);
            this.tabArchive.Name = "tabArchive";
            this.tabArchive.Padding = new System.Windows.Forms.Padding(5);
            this.tabArchive.Size = new System.Drawing.Size(992, 620);
            this.tabArchive.TabIndex = 2;
            this.tabArchive.Text = "数据存档";
            // 
            // rtbArchiveLog
            // 
            this.rtbArchiveLog.BackColor = System.Drawing.Color.Black;
            this.rtbArchiveLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbArchiveLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.rtbArchiveLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.rtbArchiveLog.Location = new System.Drawing.Point(5, 140);
            this.rtbArchiveLog.Name = "rtbArchiveLog";
            this.rtbArchiveLog.ReadOnly = true;
            this.rtbArchiveLog.Size = new System.Drawing.Size(982, 475);
            this.rtbArchiveLog.TabIndex = 0;
            this.rtbArchiveLog.Text = "";
            // 
            // panelArchiveSettings
            // 
            this.panelArchiveSettings.Controls.Add(this.lblIntervalArchive);
            this.panelArchiveSettings.Controls.Add(this.nudArchiveInterval);
            this.panelArchiveSettings.Controls.Add(this.lblSecArchive);
            this.panelArchiveSettings.Controls.Add(this.lblArchiveDir);
            this.panelArchiveSettings.Controls.Add(this.txtArchiveDir);
            this.panelArchiveSettings.Controls.Add(this.btnBrowseArchiveDir);
            this.panelArchiveSettings.Controls.Add(this.btnStartArchive);
            this.panelArchiveSettings.Controls.Add(this.btnStopArchive);
            this.panelArchiveSettings.Controls.Add(this.btnArchiveNow);
            this.panelArchiveSettings.Controls.Add(this.lblLastArchive);
            this.panelArchiveSettings.Controls.Add(this.lblNextArchive);
            this.panelArchiveSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelArchiveSettings.Location = new System.Drawing.Point(5, 5);
            this.panelArchiveSettings.Name = "panelArchiveSettings";
            this.panelArchiveSettings.Size = new System.Drawing.Size(982, 135);
            this.panelArchiveSettings.TabIndex = 1;
            // 
            // lblIntervalArchive
            // 
            this.lblIntervalArchive.AutoSize = true;
            this.lblIntervalArchive.Location = new System.Drawing.Point(10, 12);
            this.lblIntervalArchive.Name = "lblIntervalArchive";
            this.lblIntervalArchive.Size = new System.Drawing.Size(79, 17);
            this.lblIntervalArchive.TabIndex = 0;
            this.lblIntervalArchive.Text = "存档间隔(秒):";
            // 
            // nudArchiveInterval
            // 
            this.nudArchiveInterval.Location = new System.Drawing.Point(110, 9);
            this.nudArchiveInterval.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.nudArchiveInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudArchiveInterval.Name = "nudArchiveInterval";
            this.nudArchiveInterval.Size = new System.Drawing.Size(70, 23);
            this.nudArchiveInterval.TabIndex = 1;
            this.nudArchiveInterval.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // lblSecArchive
            // 
            this.lblSecArchive.AutoSize = true;
            this.lblSecArchive.Location = new System.Drawing.Point(185, 12);
            this.lblSecArchive.Name = "lblSecArchive";
            this.lblSecArchive.Size = new System.Drawing.Size(20, 17);
            this.lblSecArchive.TabIndex = 2;
            this.lblSecArchive.Text = "秒";
            // 
            // lblArchiveDir
            // 
            this.lblArchiveDir.AutoSize = true;
            this.lblArchiveDir.Location = new System.Drawing.Point(10, 45);
            this.lblArchiveDir.Name = "lblArchiveDir";
            this.lblArchiveDir.Size = new System.Drawing.Size(59, 17);
            this.lblArchiveDir.TabIndex = 3;
            this.lblArchiveDir.Text = "存档目录:";
            // 
            // txtArchiveDir
            // 
            this.txtArchiveDir.Location = new System.Drawing.Point(80, 42);
            this.txtArchiveDir.Name = "txtArchiveDir";
            this.txtArchiveDir.Size = new System.Drawing.Size(350, 23);
            this.txtArchiveDir.TabIndex = 4;
            // 
            // btnBrowseArchiveDir
            // 
            this.btnBrowseArchiveDir.Location = new System.Drawing.Point(440, 41);
            this.btnBrowseArchiveDir.Name = "btnBrowseArchiveDir";
            this.btnBrowseArchiveDir.Size = new System.Drawing.Size(60, 25);
            this.btnBrowseArchiveDir.TabIndex = 5;
            this.btnBrowseArchiveDir.Text = "浏览";
            this.btnBrowseArchiveDir.Click += new System.EventHandler(this.btnBrowseArchiveDir_Click);
            // 
            // btnStartArchive
            // 
            this.btnStartArchive.Location = new System.Drawing.Point(10, 75);
            this.btnStartArchive.Name = "btnStartArchive";
            this.btnStartArchive.Size = new System.Drawing.Size(90, 30);
            this.btnStartArchive.TabIndex = 6;
            this.btnStartArchive.Text = "开始存档";
            this.btnStartArchive.Click += new System.EventHandler(this.btnStartArchive_Click);
            // 
            // btnStopArchive
            // 
            this.btnStopArchive.Enabled = false;
            this.btnStopArchive.Location = new System.Drawing.Point(110, 75);
            this.btnStopArchive.Name = "btnStopArchive";
            this.btnStopArchive.Size = new System.Drawing.Size(90, 30);
            this.btnStopArchive.TabIndex = 7;
            this.btnStopArchive.Text = "停止存档";
            this.btnStopArchive.Click += new System.EventHandler(this.btnStopArchive_Click);
            // 
            // btnArchiveNow
            // 
            this.btnArchiveNow.Location = new System.Drawing.Point(210, 75);
            this.btnArchiveNow.Name = "btnArchiveNow";
            this.btnArchiveNow.Size = new System.Drawing.Size(90, 30);
            this.btnArchiveNow.TabIndex = 8;
            this.btnArchiveNow.Text = "立即存档";
            this.btnArchiveNow.Click += new System.EventHandler(this.btnArchiveNow_Click);
            // 
            // lblLastArchive
            // 
            this.lblLastArchive.AutoSize = true;
            this.lblLastArchive.Location = new System.Drawing.Point(10, 110);
            this.lblLastArchive.Name = "lblLastArchive";
            this.lblLastArchive.Size = new System.Drawing.Size(81, 17);
            this.lblLastArchive.TabIndex = 9;
            this.lblLastArchive.Text = "最后存档：—";
            // 
            // lblNextArchive
            // 
            this.lblNextArchive.AutoSize = true;
            this.lblNextArchive.Location = new System.Drawing.Point(160, 110);
            this.lblNextArchive.Name = "lblNextArchive";
            this.lblNextArchive.Size = new System.Drawing.Size(81, 17);
            this.lblNextArchive.TabIndex = 10;
            this.lblNextArchive.Text = "下次存档：—";
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.webBrowserChart);
            this.tabChart.Controls.Add(this.panelChartTop);
            this.tabChart.Location = new System.Drawing.Point(4, 26);
            this.tabChart.Name = "tabChart";
            this.tabChart.Padding = new System.Windows.Forms.Padding(5);
            this.tabChart.Size = new System.Drawing.Size(992, 620);
            this.tabChart.TabIndex = 4;
            this.tabChart.Text = "分时K线图";
            this.tabChart.UseVisualStyleBackColor = true;
            // 
            // webBrowserChart
            // 
            this.webBrowserChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserChart.Location = new System.Drawing.Point(5, 50);
            this.webBrowserChart.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserChart.Name = "webBrowserChart";
            this.webBrowserChart.Size = new System.Drawing.Size(982, 565);
            this.webBrowserChart.TabIndex = 0;
            // 
            // panelChartTop
            // 
            this.panelChartTop.Controls.Add(this.btnRefreshChart);
            this.panelChartTop.Controls.Add(this.lblChartHint);
            this.panelChartTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChartTop.Location = new System.Drawing.Point(5, 5);
            this.panelChartTop.Name = "panelChartTop";
            this.panelChartTop.Size = new System.Drawing.Size(982, 45);
            this.panelChartTop.TabIndex = 1;
            // 
            // btnRefreshChart
            // 
            this.btnRefreshChart.Location = new System.Drawing.Point(5, 8);
            this.btnRefreshChart.Name = "btnRefreshChart";
            this.btnRefreshChart.Size = new System.Drawing.Size(90, 28);
            this.btnRefreshChart.TabIndex = 0;
            this.btnRefreshChart.Text = "刷新图表";
            this.btnRefreshChart.UseVisualStyleBackColor = true;
            this.btnRefreshChart.Click += new System.EventHandler(this.btnRefreshChart_Click);
            // 
            // lblChartHint
            // 
            this.lblChartHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChartHint.AutoEllipsis = true;
            this.lblChartHint.Location = new System.Drawing.Point(110, 12);
            this.lblChartHint.Name = "lblChartHint";
            this.lblChartHint.Size = new System.Drawing.Size(867, 20);
            this.lblChartHint.TabIndex = 1;
            this.lblChartHint.Text = "请在「数据加载」页加载数据后，在此查看分时K线图。";
            // 
            // tabLoader
            // 
            this.tabLoader.Controls.Add(this.dgvLoadData);
            this.tabLoader.Controls.Add(this.panelLoadTop);
            this.tabLoader.Controls.Add(this.lblLoadStatus);
            this.tabLoader.Location = new System.Drawing.Point(4, 26);
            this.tabLoader.Name = "tabLoader";
            this.tabLoader.Padding = new System.Windows.Forms.Padding(5);
            this.tabLoader.Size = new System.Drawing.Size(992, 620);
            this.tabLoader.TabIndex = 3;
            this.tabLoader.Text = "数据加载";
            // 
            // dgvLoadData
            // 
            this.dgvLoadData.AllowUserToAddRows = false;
            this.dgvLoadData.AllowUserToDeleteRows = false;
            this.dgvLoadData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoadData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLdCode,
            this.colLdName,
            this.colLdCollectedAt,
            this.colLdDate,
            this.colLdTime,
            this.colLdCurrent,
            this.colLdOpen,
            this.colLdPreClose,
            this.colLdHigh,
            this.colLdLow,
            this.colLdVol,
            this.colLdTurnover,
            this.colLdBuy1Vol,
            this.colLdBuy1Price,
            this.colLdSell1Vol,
            this.colLdSell1Price,
            this.colLdStatus});
            this.dgvLoadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoadData.Location = new System.Drawing.Point(5, 50);
            this.dgvLoadData.Name = "dgvLoadData";
            this.dgvLoadData.ReadOnly = true;
            this.dgvLoadData.Size = new System.Drawing.Size(982, 543);
            this.dgvLoadData.TabIndex = 0;
            // 
            // colLdCode
            // 
            this.colLdCode.HeaderText = "代码";
            this.colLdCode.Name = "colLdCode";
            this.colLdCode.ReadOnly = true;
            // 
            // colLdName
            // 
            this.colLdName.HeaderText = "名称";
            this.colLdName.Name = "colLdName";
            this.colLdName.ReadOnly = true;
            // 
            // colLdCollectedAt
            // 
            this.colLdCollectedAt.FillWeight = 120F;
            this.colLdCollectedAt.HeaderText = "采集时间";
            this.colLdCollectedAt.Name = "colLdCollectedAt";
            this.colLdCollectedAt.ReadOnly = true;
            // 
            // colLdDate
            // 
            this.colLdDate.HeaderText = "交易日期";
            this.colLdDate.Name = "colLdDate";
            this.colLdDate.ReadOnly = true;
            // 
            // colLdTime
            // 
            this.colLdTime.HeaderText = "交易时间";
            this.colLdTime.Name = "colLdTime";
            this.colLdTime.ReadOnly = true;
            // 
            // colLdCurrent
            // 
            this.colLdCurrent.HeaderText = "现价";
            this.colLdCurrent.Name = "colLdCurrent";
            this.colLdCurrent.ReadOnly = true;
            // 
            // colLdOpen
            // 
            this.colLdOpen.HeaderText = "今开";
            this.colLdOpen.Name = "colLdOpen";
            this.colLdOpen.ReadOnly = true;
            // 
            // colLdPreClose
            // 
            this.colLdPreClose.HeaderText = "昨收";
            this.colLdPreClose.Name = "colLdPreClose";
            this.colLdPreClose.ReadOnly = true;
            // 
            // colLdHigh
            // 
            this.colLdHigh.HeaderText = "最高";
            this.colLdHigh.Name = "colLdHigh";
            this.colLdHigh.ReadOnly = true;
            // 
            // colLdLow
            // 
            this.colLdLow.HeaderText = "最低";
            this.colLdLow.Name = "colLdLow";
            this.colLdLow.ReadOnly = true;
            // 
            // colLdVol
            // 
            this.colLdVol.HeaderText = "成交量";
            this.colLdVol.Name = "colLdVol";
            this.colLdVol.ReadOnly = true;
            // 
            // colLdTurnover
            // 
            this.colLdTurnover.HeaderText = "成交额";
            this.colLdTurnover.Name = "colLdTurnover";
            this.colLdTurnover.ReadOnly = true;
            // 
            // colLdBuy1Vol
            // 
            this.colLdBuy1Vol.HeaderText = "买一量";
            this.colLdBuy1Vol.Name = "colLdBuy1Vol";
            this.colLdBuy1Vol.ReadOnly = true;
            // 
            // colLdBuy1Price
            // 
            this.colLdBuy1Price.HeaderText = "买一价";
            this.colLdBuy1Price.Name = "colLdBuy1Price";
            this.colLdBuy1Price.ReadOnly = true;
            // 
            // colLdSell1Vol
            // 
            this.colLdSell1Vol.HeaderText = "卖一量";
            this.colLdSell1Vol.Name = "colLdSell1Vol";
            this.colLdSell1Vol.ReadOnly = true;
            // 
            // colLdSell1Price
            // 
            this.colLdSell1Price.HeaderText = "卖一价";
            this.colLdSell1Price.Name = "colLdSell1Price";
            this.colLdSell1Price.ReadOnly = true;
            // 
            // colLdStatus
            // 
            this.colLdStatus.FillWeight = 50F;
            this.colLdStatus.HeaderText = "状态码";
            this.colLdStatus.Name = "colLdStatus";
            this.colLdStatus.ReadOnly = true;
            // 
            // panelLoadTop
            // 
            this.panelLoadTop.Controls.Add(this.txtLoadPath);
            this.panelLoadTop.Controls.Add(this.btnBrowseFile);
            this.panelLoadTop.Controls.Add(this.btnBrowseDir);
            this.panelLoadTop.Controls.Add(this.btnLoad);
            this.panelLoadTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLoadTop.Location = new System.Drawing.Point(5, 5);
            this.panelLoadTop.Name = "panelLoadTop";
            this.panelLoadTop.Size = new System.Drawing.Size(982, 45);
            this.panelLoadTop.TabIndex = 1;
            // 
            // txtLoadPath
            // 
            this.txtLoadPath.Location = new System.Drawing.Point(5, 10);
            this.txtLoadPath.Name = "txtLoadPath";
            this.txtLoadPath.Size = new System.Drawing.Size(400, 23);
            this.txtLoadPath.TabIndex = 0;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(415, 9);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(80, 25);
            this.btnBrowseFile.TabIndex = 1;
            this.btnBrowseFile.Text = "浏览文件";
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // btnBrowseDir
            // 
            this.btnBrowseDir.Location = new System.Drawing.Point(505, 9);
            this.btnBrowseDir.Name = "btnBrowseDir";
            this.btnBrowseDir.Size = new System.Drawing.Size(80, 25);
            this.btnBrowseDir.TabIndex = 2;
            this.btnBrowseDir.Text = "浏览目录";
            this.btnBrowseDir.Click += new System.EventHandler(this.btnBrowseDir_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(595, 9);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(60, 25);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "加载";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblLoadStatus
            // 
            this.lblLoadStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLoadStatus.Location = new System.Drawing.Point(5, 593);
            this.lblLoadStatus.Name = "lblLoadStatus";
            this.lblLoadStatus.Size = new System.Drawing.Size(982, 22);
            this.lblLoadStatus.TabIndex = 2;
            this.lblLoadStatus.Text = "请选择文件或目录后点击加载。";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "股票数据实时采集存档系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabStockMgr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocks)).EndInit();
            this.panelStockButtons.ResumeLayout(false);
            this.tabCollect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiveData)).EndInit();
            this.panelCollectSettings.ResumeLayout(false);
            this.panelCollectSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCollectInterval)).EndInit();
            this.tabArchive.ResumeLayout(false);
            this.panelArchiveSettings.ResumeLayout(false);
            this.panelArchiveSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArchiveInterval)).EndInit();
            this.tabChart.ResumeLayout(false);
            this.panelChartTop.ResumeLayout(false);
            this.tabLoader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadData)).EndInit();
            this.panelLoadTop.ResumeLayout(false);
            this.panelLoadTop.PerformLayout();
            this.ResumeLayout(false);

        }

        // ---- Tab Control ----
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabStockMgr;
        private System.Windows.Forms.TabPage tabCollect;
        private System.Windows.Forms.TabPage tabArchive;
        private System.Windows.Forms.TabPage tabLoader;
        private System.Windows.Forms.TabPage tabChart;
        private System.Windows.Forms.WebBrowser webBrowserChart;
        private System.Windows.Forms.Panel panelChartTop;
        private System.Windows.Forms.Button btnRefreshChart;
        private System.Windows.Forms.Label lblChartHint;

        // ---- Tab 1 ----
        private System.Windows.Forms.DataGridView dgvStocks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.Panel panelStockButtons;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.Button btnEditStock;
        private System.Windows.Forms.Button btnDeleteStock;

        // ---- Tab 2 ----
        private System.Windows.Forms.Panel panelCollectSettings;
        private System.Windows.Forms.Label lblIntervalCollect;
        private System.Windows.Forms.NumericUpDown nudCollectInterval;
        private System.Windows.Forms.Label lblSecCollect;
        private System.Windows.Forms.Button btnStartCollect;
        private System.Windows.Forms.Button btnStopCollect;
        private System.Windows.Forms.Label lblCollectStatus;
        private System.Windows.Forms.Label lblLastCollect;
        private System.Windows.Forms.Label lblNextCollect;
        private System.Windows.Forms.Label lblCacheCount;
        private System.Windows.Forms.CheckBox chkTimeRestrict;
        private System.Windows.Forms.DataGridView dgvLiveData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLivePreClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveHigh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveLow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveVol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveTurnover;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiveTime;

        // ---- Tab 3 ----
        private System.Windows.Forms.Panel panelArchiveSettings;
        private System.Windows.Forms.Label lblIntervalArchive;
        private System.Windows.Forms.NumericUpDown nudArchiveInterval;
        private System.Windows.Forms.Label lblSecArchive;
        private System.Windows.Forms.Label lblArchiveDir;
        private System.Windows.Forms.TextBox txtArchiveDir;
        private System.Windows.Forms.Button btnBrowseArchiveDir;
        private System.Windows.Forms.Button btnStartArchive;
        private System.Windows.Forms.Button btnStopArchive;
        private System.Windows.Forms.Button btnArchiveNow;
        private System.Windows.Forms.Label lblLastArchive;
        private System.Windows.Forms.Label lblNextArchive;
        private System.Windows.Forms.RichTextBox rtbArchiveLog;

        // ---- Tab 4 ----
        private System.Windows.Forms.Panel panelLoadTop;
        private System.Windows.Forms.TextBox txtLoadPath;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Button btnBrowseDir;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvLoadData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdCollectedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdPreClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdHigh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdLow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdVol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdTurnover;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdBuy1Vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdBuy1Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdSell1Vol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdSell1Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLdStatus;
        private System.Windows.Forms.Label lblLoadStatus;
    }
}
