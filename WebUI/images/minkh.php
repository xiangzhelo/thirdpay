	var HZ6D_CONFIGS = {
		'market_host': "http://cs.53kf.com",
		'master_host': "http://chat53kf.com",
		'talk_host': "http://www3.53kf.com",
		'base_host': "53kf.com",
		'style_id': "1",
		'com_id': "1195701",
		'guest_id': "",
		'kf':'',
		'openurl': openurl_minkh,
		'flashingInterval' : {},
		'newMsgInterval' : 0,
		'chatting_comid' : [],
		'firstopen':1,
		'loadedfav':0,
		'getcominfo':{},
		'getworkerinfo':{},
		'waitcoms':[],
		'minkh_params': minkh_params
	};
	var HZ6D_VARS = {
		'd': document,
		'dd': document.documentElement,
		'db': document.body,
		'head': document.getElementsByTagName('head')[0] || document.documentElement,
		'isStrict': document.compatMode == "CSS1Compat",
		'm': Math.max,
		'na': navigator.userAgent.toLowerCase(),
		'ie': !!document.all,
		'wlh': window.location.host
	};
	var HZ6D_TMP_VARS = {};
	String.prototype.hz6dEncode = function(){
		var txtArr = this.split('');
		txtLength = txtArr.length,
		speTxt = "-_.!~*'()",
		speArr =['2D', '5F', '2E', '21', '7E', '2A', '27', '28', '29'],
		uriEncode = '';
		for (var i = 0; i < txtLength; i++){
			var tmp_txt = txtArr[i],
					speTxtIndex = speTxt.indexOf(tmp_txt);
			if (speTxtIndex > -1) {
				uriEncode += '%' + speArr[speTxtIndex];
			} else {
				uriEncode += encodeURIComponent(tmp_txt);
			}
		}
		return uriEncode;
	}

	String.prototype.hz6dDecode = function(){
		return decodeURIComponent(this);
	}
	
	hz6d_creElm({
			href: HZ6D_CONFIGS.talk_host + '/minkh/style/client.css?2013013001',
			rel: 'stylesheet',
			type: 'text/css'
	}, 'link',HZ6D_VARS.head,1);

	hz6d_creElm({
		id: 'hz6d_iframe_proxy',
		name: 'hz6d_iframe_proxy',
		style:'position:relative;height:0;width:0;border:0;display:none;',
		frameBorder:0,
		src: HZ6D_CONFIGS.talk_host + '/minkh/hz6d_iframe_proxy.html?from{' + window.location.href.split('#6d')[0] + '}morf#hz6d{data:""}d6zh&ver=20121128001'
	}, "iframe");
	Array.prototype.indexOf = function(val) {
		for (var i = 0; i < this.length; i++) {
			if (this[i] == val) return i;
		}
		return -1;
	};
	Array.prototype.remove = function(val) {
		var index = this.indexOf(val);
		if (index > -1) {
			this.splice(index, 1);
		} else {
			this.pop();
		}
	};
	/**
		 * 克隆一个对象/函数
		 * @param Obj
		 * @returns
		 */
	function hz6d_clone_obj(Obj) {
		var buf;
		if (typeof Obj == 'array') {
			buf = [];	//创建一个空的数组
			var i = Obj.length;
			while (i--) {
				buf[i] = hz6d_clone_obj(Obj[i]);
			}
			return buf;
		}else if (typeof Obj == 'object' && Obj !== null){
			buf = {};	//创建一个空对象
			for (var k in Obj) {	//为这个对象添加新的属性
				if (!Obj.hasOwnProperty(k)) continue;
				buf[k] = hz6d_clone_obj(Obj[k]);
			}
			return buf;
		}else if (typeof Obj == 'function') {
			return (new Obj()).constructor;
		}
		else{
			return Obj;
		}
	}

	function hz6d_objAddData(obj,val) {
		for (var i in val){
			if (val.hasOwnProperty(i)) obj[i] = val[i];
		}
	}
	function hz6d_get_type(o)
	{
		var _t;
		return ((_t = typeof(o)) == "object" ? o==null && "null" || Object.prototype.toString.call(o).slice(8,-1):_t).toLowerCase();
	}
	function hz6d_ID(id)
	{
		return document.getElementById(id) || null;
	}
	function hz6d_TN(tg)
	{
		return document.getElementsByTagName(tg);
	}
	function hz6d_NM(n)
	{
		return document.getElementsByName(n);
	}

	var minkh_get_guest_id_timer = setInterval(function(){
		HZ6D_CONFIGS.guest_id = hz6d_guest_id[0];
		if (HZ6D_CONFIGS.guest_id != '' && HZ6D_CONFIGS.guest_id != 0) {
			clearInterval(minkh_get_guest_id_timer);
		}
	}, 1000);

/*	// 数据格式样板

	// 收藏夹数据索引
	var hz6d_fav_companys = {
		'0' : ['默认分组',[123,144]],//本属性是固定的，用来保存未分组的
		'group_id' : ['分组二',[233,255]],
		'33' : ['分组三',[323,344]],
		'tmpid':[]//本属性是固定的，1:用来保存临时新建的分组名称,得到group_id 后置为空 2:或者删除分组时置为要删除的group_id
	};

	// 最近联系人列表com_ids
	var hz6d_recentContact = [112,22,33,222];

	// 收藏夹中、联系人、最近联系人中的企业信息总数据,动态加载各属性及值
	var hz6d_coms_info = {'com_id1':{name:'快服科技',nick:'备注名一',logo:'minikf/img/5.jpg',addr:'tianmushan',tel:'123123123',site:'http://www.53kf.com',intro:'53快服软件超市提供的SAAS在线软件包括：客服系统、企业通讯、400电话、CRM……',status:'4',cert:1,host:'www1'},
									 '2':{name:'杭州6度',nick:'备注名一',logo:'minikf/img/5.jpg',addr:'山水',tel:'456456456',site:'http://www.6du.com',intro:'杭州6度杭州6度杭州6度杭州6度杭州6度杭州6度：客服系统、企业通讯、400电话、CRM……',status:'1',cert:0,host:'www2'},
									 'com_id2':{name:'aaaa',nick:'备注名一',logo:'minikf/img/5.jpg',addr:'aaa',tel:'aaa',site:'aaaa',intro:'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa',status:'1',cert:0,host:'www1'},
									 '12':{name:'bbb',nick:'备注名一',logo:'minikf/img/5.jpg',addr:'bbb',tel:'bbb',site:'bbbb',intro:'bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb',status:'1',cert:1,host:'www1',style:'1'}
									};

	// 正在对话的员工信息
	var hz6d_workers_info = {comid:{id:'333', name:'aaa',tel:'0579-12332112',mobi:'13030301000',email:'53kf@qq.com',msn:'6du',qq:'',sex:'女',birth:'08-08-08'},
											comid2:{id:'111', name:'bbb',tel:'0579-12332112',mobi:'13030301000',email:'53kf@qq.com',msn:'6du',qq:'',sex:'女',birth:'08-08-08'}
										 };
*/



// 初始化值
	var hz6d_fav_companys = {		// 收藏夹数据索引
			 '0' : ['默认分组',[]],				 // 本属性是固定的，用来保存未分组的
			 'tmpid':[]				// 本属性是固定的，用途:
												 // 1:新建分组名称
												 //2:删除分组的group_id
												 //3:编辑分组group_id,group_name
												 //4:删除分组时置为要删除的group_id 分组
			},
			hz6d_coms_info = {
			'1195701':{}
			},		 // 收藏夹中、联系人、最近联系人中的企业信息总数据,动态加载各属性及值
			hz6d_recentContact = [], // 最近联系人列表com_ids
			hz6d_workers_info = {},	// 正在对话的员工信息
			hz6d_talk_host = HZ6D_CONFIGS.talk_host,		// 正在访问的客户网站所在分组服务器
			hz6d_this_host = hz6d_talk_host.split('.')[0].split('/').pop();

	// 发送客户的企业收藏夹数据
	function hz6d_sendKhFavData(opts, callback) {
		/*
			opts = {grpid:group_id, grpnm:group_name, comid:com_id, comnn:com_nickname, act:act}

			guest_id 客户id
			group_id 分组id （添加分组时，没有id，id为0）
			group_name 分组名称
			com_id 公司id
			com_name 公司名称
			com_nickname 公司备注名称
			act 行为动作 包括
									'GET' : // 获取收藏夹全部数据
									'ADD_GRP' : //添加分组
									'DEL_GRP' : //删除分组
									'EDIT_GRP' : // 编辑分组名称
									'DEL_COM' : // 删除收藏企业
									'ADD_COM' : //添加收藏企业
									'EDIT_COM' ://编辑企业备注名称
									'MOVE_COM' ://移到到其他分组
									'RCCT' : // 获取最近联系人
									'DEL_RCCT': // 删除最近联系人
		*/
		opts = opts || {};
		callback = callback || function(){};
		var guest_id = HZ6D_CONFIGS.guest_id,
				group_id = opts.grpid || 0,
				group_name = opts.grpnm || '新建分组';
				act = (opts.act || 'GET').toUpperCase();
		var data = {'cmd':'KHFV','act':act,'khid':guest_id};
		switch (act)
		{
			case 'GET' : // 获取收藏夹全部数据
				// 在此添加业务代码或调用其他函数
				break;
			case 'ADD_GRP' : //添加分组
				var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_name]
				tmp[0] = group_name;// 将新建分组名称赋给临时分组

				hz6d_objAddData(data,{'grpnm':group_name.hz6dEncode()});
				// 在此添加业务代码或调用其他函数
				break;
			case 'DEL_GRP' : //删除分组
				if (group_id != '0') {
					var tmp = hz6d_fav_companys['tmpid'];
					tmp[0] = group_id; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id]

					hz6d_objAddData(data,{'grpid':group_id});
				// 在此添加业务代码或调用其他函数
				} else {
					hz6d_alert('默认分组不能删除');
				}
				break;
			case 'EDIT_GRP' : // 编辑分组名称
				var tmp = hz6d_fav_companys['tmpid'];
				tmp[0] = group_id; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id,group_name]
				tmp[1] = group_name;

				hz6d_objAddData(data,{'grpid':group_id,'grpnm':group_name.hz6dEncode()});
				// 在此添加业务代码或调用其他函数
				break;
			case 'DEL_COM' : // 删除收藏企业
				var com_id = opts.comid;
				var tmp = hz6d_fav_companys['tmpid'];
				tmp[0] = group_id; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id,com_id]
				tmp[1] = com_id;

				hz6d_objAddData(data,{'grpid':group_id,'comid':com_id});
				// 在此添加业务代码或调用其他函数
				break;
			case 'ADD_COM' : // 添加收藏企业
				var com_id = opts.comid,
						cert = (typeof(hz6d_coms_info[com_id].cert) != 'undefined') ? hz6d_coms_info[com_id].cert : (com_is_certified(com_id) || 0),
						host = hz6d_coms_info[com_id].host || hz6d_this_host,
						style = hz6d_coms_info[com_id].style || HZ6D_CONFIGS.style_id,
						com_name = hz6d_coms_info[com_id].name,
						com_nickname = opts.comnn || com_name || '-';
				hz6d_coms_info[com_id].cert = cert;
				var tmp = hz6d_fav_companys['tmpid'];
				tmp[0] = group_id; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id,com_id,com_nickname]
				tmp[1] = com_id;
				tmp[2] = com_nickname;

				hz6d_objAddData(data,{'grpid':group_id,'comid':com_id,'comnm':com_name.hz6dEncode(),'comnn':com_nickname.hz6dEncode(),'cert':cert,'host':host,'style':style});

				// 在此添加业务代码或调用其他函数
				break;
			case 'EDIT_COM' : // EDIT_COM编辑
				var com_id = opts.comid,
						com_nickname = opts.comnn;
				var tmp = hz6d_fav_companys['tmpid'];
				tmp[0] = com_id; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [com_id,com_nickname]
				tmp[1] = com_nickname;

				hz6d_objAddData(data,{'grpid':group_id,'comid':com_id,'comnn':com_nickname.hz6dEncode()});
				// 在此添加业务代码或调用其他函数
				break;
			case 'MOVE_COM' : // 移到到其他分组
				var com_id = opts.comid;
				var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [com_id,from_group_id,to_group_id]
				tmp[0] = com_id;
				for(var i in hz6d_fav_companys){
					if (!hz6d_fav_companys.hasOwnProperty(i)) continue;
					try{
						if (hz6d_fav_companys[i][1].indexOf(com_id) > -1){
							tmp[1] = i;
							break;
						}
					} catch(e){}
				}
				tmp[2] = group_id;

				var group_id_from = tmp[1], group_id_to = tmp[2];
				hz6d_objAddData(data,{'fgrpid':group_id_from,'tgrpid':group_id_to,'comid':com_id});
				// 在此添加业务代码或调用其他函数
				break;
			case 'RCCT' : // 获取最近联系人
				// 在此添加业务代码或调用其他函数
				break;
			case 'DEL_RCCT' : // 删除最近联系人
				var com_id = opts.comid;
				var tmp = hz6d_fav_companys['tmpid'];
				tmp[0] = com_id; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [com_id]

				hz6d_objAddData(data,{'comid':com_id});
				// 在此添加业务代码或调用其他函数
				break;
			default : // 其他情况失败
				/*fav = 'fail' */
				// 在此添加业务代码或调用其他函数
				hz6d_alert('操作失败，请再次操作');
				break;
		}
		var curSecond = Math.floor(((new Date()).getTime())/1000);
		hz6d_objAddData(data,{'did':1,'sid':13,'time':curSecond});
		//alert(data.toSource()); //return;
		var url = hz6d_talk_host + '/sendacc.jsp?jsoncallback=?';
		try {
			url += '&' + hz6d_join_json('&',data);
			flp.getJSON(url, function(json){
				hz6d_recvKhFavData.call(this, json, callback);
			});
		} catch(e) {}
	}

	/* 获取企业信息命令*/
	function hz6d_getCompanyInfo(com_id, callback) {
		callback = callback || function(){};
		if (!!HZ6D_CONFIGS.getcominfo[com_id]) {
			callback(1, com_id);
		} else {
			try {
				//if (typeof hz6d_coms_info[com_id] == 'undefined') hz6d_coms_info[com_id] = {};
				var style_id = !hz6d_coms_info[com_id] ? HZ6D_CONFIGS.style_id : hz6d_coms_info[com_id].style || HZ6D_CONFIGS.style_id,
						host = !hz6d_coms_info[com_id] ? hz6d_this_host : hz6d_coms_info[com_id].host || hz6d_this_host,
						url = 'http://' + host + '.' + HZ6D_CONFIGS.base_host + '/impl/rpc_company_info_minkh.php?jsoncallback=?',
						data = {'check_id':'11917718fe939f3106d35a30074bcd30', 'company_id':com_id, 'style_id':style_id};
				url += '&' + hz6d_join_json('&',data);
				flp.getJSON(url, function(json){
					/*
					info = [
									'com_id'
									'name',
									'addr',//企业地址
									'tel',//电话
									'site',//网址
									'intro'//简介
								 ]
					*/
					// 整合完善到企业信息的json数据中
					//alert(json);
					var info = (new Function('',"return " + json))(); // string -> array
					var arr = ['com_id','name','addr', 'tel', 'site', 'intro'];
					var com_id = info[0].hz6dDecode();
					if(typeof hz6d_coms_info[com_id] == 'undefined') hz6d_coms_info[com_id] = {};
					for (var i = 1; i < arr.length; i++)
					{
						hz6d_coms_info[com_id][arr[i]] = info[i].hz6dDecode().replace(/#1yin/g,"'").replace(/#2yin/g,'"');
					}
					if (typeof(hz6d_coms_info[com_id].host) == 'undefined') hz6d_coms_info[com_id].host = hz6d_this_host;
					if (typeof(hz6d_coms_info[com_id].style) == 'undefined') hz6d_coms_info[com_id].style = HZ6D_CONFIGS.style_id;
					if (typeof(hz6d_coms_info[com_id].nick) == 'undefined' || hz6d_coms_info[com_id].nick == '') hz6d_coms_info[com_id].nick =	hz6d_coms_info[com_id].name;
					if (typeof(hz6d_coms_info[com_id].logo) == 'undefined' || hz6d_coms_info[com_id].logo == '' || hz6d_coms_info[com_id].logo == 'default') hz6d_coms_info[com_id].logo =	HZ6D_CONFIGS.talk_host + "/minkh/style/53.jpg";
					if (typeof(hz6d_coms_info[com_id].site) == 'undefined' || hz6d_coms_info[com_id].site.replace('http://','').replace('https://','').replace(/#/g,'') == '') hz6d_coms_info[com_id].site = '';
					hz6d_coms_info[com_id].site = hz6d_coms_info[com_id].site.replace('http://','').replace('https://','').replace(/#/g,'');
					if(hz6d_coms_info[com_id].site != '') {
						hz6d_coms_info[com_id].site = 'http://' + hz6d_coms_info[com_id].site;
					} else {
						hz6d_coms_info[com_id].site = '--';
					}
					// 在此添加业务代码或调用其他函数

					HZ6D_CONFIGS.getcominfo[com_id] = 1;
					callback(1, com_id);
				});
			} catch(e){
				callback(0, com_id)
			}
		}
	}

	/* 获取员工名片信息命令*/
	function hz6d_getWorkerInfo(com_id, callback) {
		callback = callback || function(){};
		if (!!HZ6D_CONFIGS.getworkerinfo[com_id]) {
			callback(1, com_id);
		} else {
			try {
				var host = hz6d_coms_info[com_id].host || hz6d_this_host,
					url = 'http://' + host + '.' + HZ6D_CONFIGS.base_host + '/impl/rpc_worker_info_minkh.php?jsoncallback=?',
					worker_id = hz6d_workers_info[com_id]['id'],
					data={'check_id':'11917718fe939f3106d35a30074bcd30', 'com_id':com_id, 'worker_id':worker_id};
				url += '&' + hz6d_join_json('&',data);
				flp.getJSON(url, function(json){
					/*
					info = [
							'com_id',
							'worker_id',//kfid = id6d
							'name',//工号名称
							'tel',//电话
							'mobi',//手机
							'email',//邮箱
							'msn',
							'qq',
							'sex',
							'birth'
						 ]
					*/

					// 整合完善到员工信息的json数据中
					var info = (new Function('','return ' + json))(); //string -> array
					var com_id = info[0].hz6dDecode(), worker_id = info[1].hz6dDecode();
					if (typeof hz6d_workers_info[com_id] == 'undefined') hz6d_workers_info[com_id] = {};
					hz6d_workers_info[com_id]['id'] = worker_id;
					var arr = ['com_id','work_id','name', 'tel', 'mobi', 'email', 'msn','qq','sex','birth'];
					for (var i = 2; i < 10; i++)
					{
					hz6d_workers_info[com_id][arr[i]] = info[i].hz6dDecode();
					}

					HZ6D_CONFIGS.getworkerinfo[com_id] = 1;
					callback(1, com_id);
					// 在此添加业务代码或调用其他函数
				});
			} catch(e){callback(0, com_id)}
		}
	}
	/* setTimeout(function(){
		hz6d_getLwordComs(HZ6D_CONFIGS.guest_id,function(a,json){
			if (a == 1){
				for(var i in json){
					if(json.hasOwnProperty(i)){
						//HZ6D_CONFIGS.waitcoms.push(json[i]);
						hz6d_getCompanyInfo(json[i], function(a){
							if (a == 1) {
								hz6d_flashing(json[i]);
							}
						});
					}
				}
			}
		});
	},3000); */
	/* 获取给客户留言的公司*/
	function hz6d_getLwordComs(guest_id, callback) {
		guest_id = guest_id || HZ6D_CONFIGS.guest_id;
		callback = callback || function(){};
		if (!!HZ6D_CONFIGS.getLwordComs) {
			callback(1);
		} else{
			try {
				var url = hz6d_talk_host + '/impl/rpc_cus_web_msg_minkh.php?jsoncallback=?',
						data = {'check_id':'11917718fe939f3106d35a30074bcd30', 'guest_id':guest_id};
				url += '&' + hz6d_join_json('&',data);
				flp.getJSON(url, function(json){
					json = (new Function("","return " + json))(); // string -> json
					callback(1,json);
				});
			} catch(e){callback(0)}
		}
	}

	// 接收到客户的企业收藏夹数据
	function hz6d_recvKhFavData(json, callback)
	{
		var act = json.act || 'GET', fav = json.fav || '', contact = json.contact || '';
		switch (act)
		{
			case 'GET' : // 获取收藏夹全部数据
				/*
				// 在线状态 status : 1,2,3,4 在线，忙碌，离开，离线
				//logo 取logo图片的完整url
				//cert 表示认证状态，用1表示已认证，0表示未认证
				fav = {0:['默认分组',{comid1:['comname1','nickname1','status','logo','cert',host,style],
															comid2:['comname2','nickname2','status','logo','cert',host,style],
															comid3:['comname3','nickname3','status','logo','cert',host,style]
														 }],
						 group_id1:['好友分组',{comid4:['comname4','nickname4','status','logo','cert',host,style],
																		comid5:['comname5','nickname5','status','logo','cert',host,style]
											}]
					}
				*/
				if (fav.indexOf('fail') == -1 && fav != '') { // 将fav返回的值赋值到hz6d_fav_companys、hz6d_coms_info中
					fav = (new Function("","return " + fav))(); // string -> json
					var arr = ['name','nick','status','logo','cert','host','style'];
					for (var i in fav) {
						if (!fav.hasOwnProperty(i)) continue;
						hz6d_fav_companys[i] = [];
						hz6d_fav_companys[i][0] = fav[i][0].hz6dDecode();
						hz6d_fav_companys[i][1] = [];
						for (var j in fav[i][1]) {
							if (!fav[i][1].hasOwnProperty(j)) continue;
							hz6d_fav_companys[i][1].push(j);
							if (typeof hz6d_coms_info[j] == 'undefined') hz6d_coms_info[j] = {};
							for(var k = 0, tmp = fav[i][1][j]; k < 7; k++ ) {
								try{
									if(k == 0 || k == 1) tmp[k] = tmp[k].hz6dDecode();
								} catch(e){}
								hz6d_coms_info[j][arr[k]] = tmp[k];
							}
							if (typeof(hz6d_coms_info[j].logo) == 'undefined' || hz6d_coms_info[j].logo == '' || hz6d_coms_info[j].logo == 'default') hz6d_coms_info[j].logo =	HZ6D_CONFIGS.talk_host + "/minkh/style/53.jpg";
						}
					}
					callback(1);
				} else {
					callback(0);
				}
				break;
			case 'ADD_GRP' : //添加分组
				/*fav = group_id*/

				if (fav != 'fail') {

					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_name]
					hz6d_fav_companys[fav] = []; // 创建新分组
					hz6d_fav_companys[fav][0] = tmp[0];// 将临时分组的名称赋给新建分组
					hz6d_fav_companys[fav][1] = [];
					callback(1, fav);
					//tmp = [];
				} else {
					callback(0);
				}
				// 如果获得了group_id说明添加分组成功,需要把添加成功的分组
				break;
			case 'DEL_GRP' : //删除分组
				/*fav = 'ok'*/
				if (fav == 'ok') {
					var tmp = hz6d_fav_companys['tmpid'],group_id = tmp[0];
					if (hz6d_fav_companys[group_id][1].length > 0){ // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id]
						hz6d_fav_companys['0'][1] = (hz6d_fav_companys['0'][1]).concat(hz6d_fav_companys[group_id][1]); //将要删除的分组中的com_ids 赋值到默认分组
					}
					delete hz6d_fav_companys[group_id]; //删除分组
					callback(1);
					//tmp = [];
				} else {
					callback(0);
				}
				break;
			case 'EDIT_GRP' : // 编辑分组名称
				/*fav = 'ok' */
				if(fav == 'ok'){
					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id,group_name]
					hz6d_fav_companys[tmp[0]][0] = tmp[1]; // 将分组名称从临时分组中取出赋值到编辑的分组
					callback(1);
				 // tmp = [];
				} else {
					callback(0);
				}
				break;
			case 'DEL_COM' : // 删除收藏企业
				/*fav = 'ok' */
				if(fav == 'ok'){
					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id,com_id]
					hz6d_fav_companys[tmp[0]][1].remove(tmp[1]); // 在分组中删除收藏企业
					callback(1);
				//	tmp = [];
				} else {
					callback(0);
				}
				break;
			case 'ADD_COM' : // 添加收藏企业
				/*fav = 'ok' */
				if(fav == 'ok'){
					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [group_id,com_id]
					hz6d_fav_companys[tmp[0]][1].push(tmp[1]); // 添加企业com_id 放入分组收藏夹数据
					hz6d_coms_info[tmp[1]].nick = tmp[2];
					callback(1, hz6d_clone_obj(tmp));
					//tmp = [];
				} else {
					callback(0,fav);
				}
				break;
			case 'EDIT_COM' : // EDIT_COM编辑
				/*fav = 'ok' */
				if(fav == 'ok'){
					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [com_id,com_nickname]

					hz6d_coms_info[tmp[0]]['nick'] = tmp[1]; // 将企业备注名称从临时数据中取出赋值
					callback(1);
					//tmp = [];
				} else {
					callback(0);
				}
				break;
			case 'MOVE_COM' : // 移到到其他分组
				/*fav = 'ok' */
				if (fav == 'ok'){
					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [com_id,from_group_id,to_group_id]
					hz6d_fav_companys[tmp[2]][1].push(tmp[0]); // 插入到新分组
					hz6d_fav_companys[tmp[1]][1].remove(tmp[0]); // 从旧分组删除
					callback(1);
				 // tmp = [];
				} else {
					callback(0);
				}
				break;
			case 'RCCT' : // 获取最近联系人
				/*
				// 在线状态 status : 1,2,3,4 在线，忙碌，离开，离线
				//logo 取logo图片的完整url
				//cert 表示认证状态，用1表示已认证，0表示未认证
				guest_id = khid
				contact = {
										comid1:['comname1','nickname1','status','logo','cert','host','style'],
										comid2:['comname2','nickname2','status','logo','cert','host','style'],
										comid3:['comname3','nickname3','status','logo','cert','host','style']
									}
				*/
				if (contact !='' && contact.indexOf('fail') == -1) { // 将fav返回的值赋值到hz6d_fav_companys、hz6d_coms_info中
						contact = (new Function('','return ' + contact))();
					var arr = ['name','nick','status','logo','cert','host','style'];
					hz6d_recentContact = [];
					for (var i in contact)
					{
						if (!contact.hasOwnProperty(i)) continue;
						hz6d_recentContact.push(i);
						if (typeof hz6d_coms_info[i] == 'undefined') hz6d_coms_info[i] = {};
						for (var k = 0; k < 7; k++)
						{
							try{
								if(k == 0 || k == 1) contact[i][k] = contact[i][k].hz6dDecode();
							} catch (e){}
							hz6d_coms_info[i][arr[k]] = hz6d_coms_info[i][arr[k]] || contact[i][k];
						}
						if(!hz6d_coms_info[i].logo || hz6d_coms_info[i].logo == 'default'){
							hz6d_coms_info[i].logo = HZ6D_CONFIGS.talk_host + "/minkh/style/53.jpg";
							contact[i][3] = hz6d_coms_info[i].logo
						}
					}
					callback(1);
				} else {
					callback(0);
				}
				break;
			case 'DEL_RCCT' : // 删除最近联系人
				if (fav == 'ok'){
					var tmp = hz6d_fav_companys['tmpid']; // tmp 中保存的数据是：hz6d_fav_companys['tmpid'] = [com_id]
					hz6d_recentContact.remove(tmp[0]); // 从最近联系人中删除
					tmp = [];
					callback(1);
				} else {
					callback(0);
				}
				break;
			default : // 其他情况失败
				/*fav = 'fail' */
				callback(0);
				break;
		}
	}

	// 创建对话
	function hz6d_new_chat(com_id, kf)
	{
		kf = kf ||'';
		if(typeof hz6d_coms_info[com_id] == 'undefined') {
			hz6d_coms_info[com_id] = {};
		}
		if(typeof hz6d_coms_info[com_id]['host'] == 'undefined'){
			hz6d_coms_info[com_id]['host'] = hz6d_this_host;
			hz6d_coms_info[com_id]['style'] = '1';
		}
		var name = (hz6d_coms_info[com_id].nick ? hz6d_coms_info[com_id].nick : hz6d_coms_info[com_id].name) || '';
		if (!hz6d_ID('rcct_com_'+com_id)) flp('.mnkf_list_scroll .mnkf_list_recent ul').prepend('<li class="hz6d_kf" id="rcct_com_'+com_id+'"><div class="avatar"><a><img style="height:20px;width:20px;" src="'+hz6d_coms_info[com_id].logo+'" /></a><div class="status_icon s_'+hz6d_coms_info[com_id].status+'"></div></div><div class="mnkf_nickname">'+name+'</div><div class="btn_con" style="display:none;"><a class="del"></a></div></li>');
		if(HZ6D_CONFIGS.firstopen == 1) {
			hz6d_coms_info[com_id]['host'] = hz6d_this_host;
			hz6d_coms_info[com_id]['style'] = HZ6D_CONFIGS.style_id;
			HZ6D_CONFIGS.firstopen = 0;
		}
		try {
			// var iframes = hz6d_ID('hz6d_chatting_iframes').getElementsByTagName('iframe');
			// for (var i = 0; i < iframes.length; i++) {
				// iframes[i].style.display = 'none';
			// }
			flp('#hz6d_chatting_iframes iframe').hide();
			
			if (hz6d_ID('hz6d_chat_iframe_' + com_id)){
				flp('#hz6d_chat_iframe_' + com_id).show();
				//hz6d_ID('hz6d_chat_iframe_' + com_id).style.display = 'block';
			} else {
				//var style_display = (kf == '' || iframes.length == 0) ? 'display:block' : 'display:none';
				hz6d_creElm({
					id: 'hz6d_chat_iframe_' + com_id,
					name: 'hz6d_chat_iframe_' + com_id,
					style:'height:370px;width:336px;border:0;position:relative;top:0;left:0; overflow:hidden;visibility:visible;z-index:1;',
					frameBorder:0,
					scrolling: 'no',
					src: 'http://' + hz6d_coms_info[com_id]['host'] + '.' + HZ6D_CONFIGS.base_host + '/webClientMin.php?company_id=' + com_id + '&style=' + hz6d_coms_info[com_id]['style'] + '&kf=' + kf + '&timeStamp=' + new Date().getTime() + '&' + minkh_params
				}, "iframe",hz6d_ID('hz6d_chatting_iframes'));
			}
		} catch(e){}
	}

	//获取QueryString的数组
	function hz6d_getQueryString(){
		 var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+","g"));
		 for(var i = 0; i < result.length; i++){
					result[i] = result[i].substring(1);
		 }
		 return result;
	}

	//根据QueryString参数名称获取值
	function hz6d_getQueryStringByName(name){
		 var result = location.search.match(new RegExp("[\?\&]" + name+ "=([^\&]+)","i"));
		 if(result == null || result.length < 1){
					return "";
		 }
		 return result[1];
	}

	//根据QueryString参数索引获取值
	function hz6d_getQueryStringByIndex(index){
		 if(index == null){
					return "";
		 }
		 var queryStringList = hz6d_getQueryString();
		 if (index >= queryStringList.length){
					return "";
		 }
		 var result = queryStringList[index];
		 var startIndex = result.indexOf("=") + 1;
		 result = result.substring(startIndex);
		 return result;
	}

	function com_is_certified(company_id){
		flp.getJSON(HZ6D_CONFIGS.market_host + "/impl/company_auth_impl.php?check_id=11917718fe939f3106d35a30074bcd30&company_id=" + company_id + "&jsoncallback=?",
			function(json){
				if(json=="1"){
					hz6d_coms_info[company_id].cert = 1;
				}
				else{
					hz6d_coms_info[company_id].cert = 0;
				}
				return json;
			}
		);
	}
	/* 从跨域代理页面获取数据 */
	function recvDataFromIframeProxy() {
		try {
			//var wlh = window.frames['hz6d_iframe_proxy'].frames['kehu_iframe_proxy'].location.href,
			var wlh = window.location.href,
					hz6d_index = wlh.indexOf('hz6d{'),
					d6zh_index = wlh.indexOf('}d6zh');
			if (hz6d_index == -1 || d6zh_index == -1) return;
			window.location = wlh.replace(/#hz6d\{.*?\}d6zh/gi,'#6d');
			hz6d_index += 4;
			d6zh_index += 1;
			var my_data = wlh.substring(hz6d_index, d6zh_index).replace(/%27/g,'"').replace(/%22/g,'"');
					my_data = decodeURI(my_data);
			if ((HZ6D_TMP_VARS.iframeData != my_data)){
				var _data = (new Function('','return ' + my_data))();
				switch(_data.cmd) {
					case 'worker':
						if (typeof hz6d_workers_info[_data.comid] == 'undefined') hz6d_workers_info[_data.comid] = {};
						hz6d_workers_info[_data.comid]['id'] = _data.data;
						if(_data.data != ''){
							// 已连接
							hz6d_coms_info[_data.comid].linked = '1';
							//在线状态
							var status_icon = flp("#chat_com_" + _data.comid + " div.status_icon, #fav_com_" + _data.comid + " div.status_icon, #rcct_com_" + _data.comid + " div.status_icon");
							hz6d_coms_info[_data.comid].status = "1";
							status_icon.removeClass("s_4");
							status_icon.addClass("s_1");
							//if (flp("#chat_com_" + _data.comid).hasClass('focus')){
								flp("#chat_com_" + _data.comid + ".l_title div.status_icon").removeClass("s_4");
								flp("#chat_com_" + _data.comid + ".l_title div.status_icon").addClass("s_1");
							//}
						}
						break;
					case 'new_msg':
						hz6d_flashing(_data.comid);
						break;
					case 'ad_logo':
						if (_data.comid == HZ6D_CONFIGS.com_id) hz6d_ID('hz6d_iframe_logo').src = _data.data;
						break;
					case 'vote_fn':
						hz6d_coms_info[_data.comid].vote = _data.data;
						break;
					case 'kfcard':
						hz6d_coms_info[_data.comid].kfcard = _data.data;
						break;
					case 'voted':
						hz6d_coms_info[_data.comid].voted = _data.data;
						break;
					case 'guest_id':
						hz6d_guest_id = _data.data;
						HZ6D_CONFIGS.guest_id = _data.data;
						break;
					case 'unlink':
						hz6d_coms_info[_data.comid].linked = '0';
						break;
					case 'xlink':
						var tmp_data =  _data.data.split(',');
						//alert(tmp_data[2] + ':' + tmp_data[0]);
						add_chatting_list(tmp_data[2],'add',tmp_data[0]);
						break;
					default:
						break;
				}
				HZ6D_TMP_VARS.iframeData = my_data;
			}
		} catch(e) {}
	}
	function sendDataToIframeProxy(comid, cmd, data){
		data = data || Math.random();
		var datas = 'comid:"' + comid + '",cmd:"' + cmd + '",data:"' + data + '"';
		try {
			document.getElementById('hz6d_iframe_proxy').contentWindow.location = HZ6D_CONFIGS.talk_host + '/minkh/hz6d_iframe_proxy.html#from{' + window.location.href.split('#6d')[0] +' }morf#hz6d{' + encodeURI(datas) + '}d6zh';
		} catch(e) {}
	}

//聊天列表最大显示信息条数
var hz6d_line = 11;
//鼠标停留弹出框settimeout
var hz6d_time_alt;
//鼠标放上去的样式
var hz6d_divItemSelect = 'div_item_select';
//已知公司id，获取分组id
function get_gp_id(com_id){
	for (var i in hz6d_fav_companys){
		if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
		for (var j = 0, len = hz6d_fav_companys[i][1].length; j < len; j++) {
			if (com_id == hz6d_fav_companys[i][1][j]){
				return i;
			}
		}
	}
	return -1;
}
//加载收藏夹
function load_fav_companys(){
	var total_classify = 0, total_fav_coms = 0;
	for(var i in hz6d_fav_companys){
		if ((!hz6d_fav_companys.hasOwnProperty(i)) || (i == 'tmpid')) continue;
		try{total_fav_coms += hz6d_fav_companys[i][1].length;} catch(e){}
		total_classify++;
		if(i == "0"){//默认分组
			if (!!document.getElementById('fav_grp_'+i)) {
				flp('#fav_grp_'+i).html('<div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>'+hz6d_fav_companys[i][0]+'</span>(<em>0</em>)</div><div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a></div></div><ul></ul>');
			} else {
				flp('.mnkf_list_classify_create').before('<div class="mnkf_list_classify" id="fav_grp_'+i+'"><div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>'+hz6d_fav_companys[i][0]+'</span>(<em>0</em>)</div><div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a></div></div><ul></ul></div>');
			}
		}else{
			if (!!document.getElementById('fav_grp_'+i)) {
				flp('#fav_grp_'+i).html('<div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>'+hz6d_fav_companys[i][0]+'</span>(<em>0</em>)</div><div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a><a class="del"></a></div></div><ul></ul>');
			} else {
				flp('.mnkf_list_classify_create').before('<div class="mnkf_list_classify" id="fav_grp_'+i+'"><div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>'+hz6d_fav_companys[i][0]+'</span>(<em>0</em>)</div><div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a><a class="del"></a></div></div><ul></ul></div>');
			}
		}
		var num = 0;
		var online = 0;
		for (var j = 0, tmp_len = hz6d_fav_companys[i][1].length; j < tmp_len; j++) {
			for(var t in hz6d_coms_info){
				if (!hz6d_coms_info.hasOwnProperty(t)) continue;
				var name = hz6d_coms_info[t].nick?hz6d_coms_info[t].nick:hz6d_coms_info[t].name;
				var logo = hz6d_coms_info[t].logo;
				if(hz6d_fav_companys[i][1][j] == t){
					num = num+1;
					//if(hz6d_coms_info[t].status != 4){
						//online = online+1;
						flp('#fav_grp_'+i).find('ul').prepend('<li class="hz6d_kf" id="fav_com_'+t+'"><div class="avatar"><a><img style="height:20px;width:20px;" src="'+logo+'"></a><div class="status_icon s_' + hz6d_coms_info[t].status + '"></div></div><div class="mnkf_nickname">'+name+'</div><div class="btn_con" style="display:none;"><a class="edit"></a><a class="del"></a></div></li>');
					//}else if(coms_info[t].status == 4){
						//flp('#'+i).find('ul').append('<li class="hz6d_kf" id="'+t+'"><div class="avatar"><a><img style="height:20px;width:20px;" src="'+logo+'"></a><div class="status_icon s_'+hz6d_coms_info[t].status+'"></div></div><div class="mnkf_nickname">'+name+'</div><div class="btn_con" style="display:none;"><a class="edit"></a><a class="del"></a></div></li>');
					//}
				}
			}
		}
		flp('#fav_grp_'+i).find('em').html(num);
	}
	flp('.mnkf_min').find('.hz6d_t').html('企业收藏夹('+ total_fav_coms +')');
}
//加载最近联系人
function load_recent_contact(){
	flp('.mnkf_list_scroll .mnkf_list_recent ul').html('');
	for(var i = 0, len = hz6d_recentContact.length; i < len; i++){
		for(var j in hz6d_coms_info){
			if (!hz6d_coms_info.hasOwnProperty(j)) continue;
			if(hz6d_recentContact[i] == j){
				var name = (hz6d_coms_info[j].nick ? hz6d_coms_info[j].nick : hz6d_coms_info[j].name) || '';
				flp('.mnkf_list_scroll .mnkf_list_recent ul').append('<li class="hz6d_kf" id="rcct_com_'+j+'"><div class="avatar"><a><img style="height:20px;width:20px;" src="'+hz6d_coms_info[j].logo+'" /></a><div class="status_icon s_'+hz6d_coms_info[j].status+'"></div></div><div class="mnkf_nickname">'+name+'</div><div class="btn_con" style="display:none;"><a class="del"></a></div></li>');
			}
		}
	}
}
//鼠标移动至，出现编辑，删除图标
function showMouseover(obj){
	obj.addClass('div_item_select');
	if (HZ6D_CONFIGS.guest_id != '' && HZ6D_CONFIGS.guest_id != 0 && HZ6D_CONFIGS.loadedfav == 1) {
		obj.children('.btn_con').show();
	};
}
//鼠标移除，不显示编辑，删除图标
function showMouseout(obj){
	obj.removeClass('div_item_select');
	obj.children('.btn_con').hide();
}
//点击动作
function hz6d_showClick(obj,type,target){
	if(type == 'group'){
		if(target == 'edit'){
			obj.find('span').html('<input type="text" class="editing" value="'+obj.find('span').html()+'"/>');
			obj.find('.btn_con').remove();
			flp('.editing').focus();
			//var span = obj.find('span');
			//span.html('<input type="text" class="editing" value="'+span.html()+'"/>');
			//alert(obj.children('.btn_con').css('display'));
			//obj.children('.btn_con').css('display','none');
			//flp('.editing').focus();
		}else if(target == 'del'){
			var gp_id = obj.parent('.mnkf_list_classify').attr("id").replace('fav_grp_','');
			var id = -1;
			hz6d_showMsg(id,'delGroup',gp_id);
		}else if(target == 'editing'){
			obj.children('.btn_con').css('display','none');
		}else{
			try{
				var gp_id = obj.parent('.mnkf_list_classify').attr("id").replace('fav_grp_','');
				if(hz6d_fav_companys[gp_id][1].length > 0)
					obj.parent().toggleClass('mnkf_list_classify_show');
				else return false;
			} catch(e){}
		}
	}else if(type == 'company'){
		if(target == 'edit'){
			var id = obj.attr("id").replace('fav_com_','');
			var gp_id = obj.parents('div[class="mnkf_list_classify mnkf_list_classify_show"]').attr("id").replace('fav_grp_','');
			hz6d_showMsg(id,'editCompany',gp_id);
		}else if(target == 'del'){
			var id = obj.attr("id").replace('fav_com_','');
			var gp_id = obj.parents('div[class="mnkf_list_classify mnkf_list_classify_show"]').attr("id").replace('fav_grp_','');
			hz6d_showMsg(id,'delCompany',gp_id);
		}
	}else if(type == 'recent'){
		if(target == 'del'){
			var id = obj.attr("id").replace('rcct_com_','');
			var gp_id = get_gp_id(id);
			hz6d_showMsg(id,'delRecent',gp_id);
		}
	}
}
//双击联系人
function dblclick(obj){
	obj.dblclick(function(){
		var com_id = obj.attr("id").replace('fav_com_','');
		add_chatting_list(com_id,'add');
	});
}
// 搜索栏 键盘操作	向上 或向上	@param {Object} opt	 向上 -1	向下 1
function chageSelect(opt){
	if (flp(".mnkf_searchbox").css('display') != 'none') {
		var obj = flp(".mnkf_searchbox ul li[class='hz6d_kf " + hz6d_divItemSelect + "']");
		if (!obj.html()) {//当前还未选中
			if (opt == 1) {
				flp(".mnkf_searchbox ul li:first").addClass(hz6d_divItemSelect);
			}
			else {
				flp(".mnkf_searchbox ul li:last").addClass(hz6d_divItemSelect);
			}
		} else {
			obj.removeClass(hz6d_divItemSelect);
			var curr = obj.index() + opt;
			//var curr = parseInt(obj.children('input').val()) + opt;
			var divCount = flp(".mnkf_searchbox ul li").length;
			
			if(curr < 0){
				curr = divCount-1;
			}else if(curr == divCount){
				curr = 0;
			}
			flp(".mnkf_searchbox ul li:eq(" + curr + ")").addClass(hz6d_divItemSelect);
		}
	}
}
//各种弹出层
function hz6d_showMsg(id,type,gp_id){
	var clientWidth = 617, clientHeight = 400;
	var title = '';
	var content = '';
	if(type == 'delGroup'){
		title = '删除分组';
		content = '选定的分组将被删除，<br />组内联系人将会移至系统默认分组“' + hz6d_fav_companys[0][0] + '“。<br />您确定要删除该分组吗？';
	}else if(type == 'delRecent'){
		var name = hz6d_coms_info[id].nick?hz6d_coms_info[id].nick:hz6d_coms_info[id].name;
		title = '删除最近联系人';
		content = '您确定从最近联系人列表中删除：<img style="height:20px;width:20px;" src="' + hz6d_coms_info[id].logo + '"/>&nbsp;<b>'+name+'</b>&nbsp;吗？';
	}else{
		var name = hz6d_coms_info[id].nick?hz6d_coms_info[id].nick:hz6d_coms_info[id].name;
		if(type == 'delCompany'){
			title = '删除企业';
			content = '您确定要从企业收藏夹中删除：<img style="height:20px;width:20px;" src="' + hz6d_coms_info[id].logo + '"/>&nbsp;<b>'+name+'</b>&nbsp;吗？';
		}else if(type == 'editCompany'){
			title = '编辑企业';
			var option = '';
			for(var i in hz6d_fav_companys){
				if (hz6d_fav_companys.hasOwnProperty(i) && i!= 'tmpid'){
					if(i == gp_id){
						option += '<option value="'+i+'" selected>'+hz6d_fav_companys[i][0]+'</option>';
					}else{
						option += '<option value="'+i+'">'+hz6d_fav_companys[i][0]+'</option>';
					}
				}
			}
			content = '<div class="addFriendBox">'+
				'<table class="addFriendTable" border="0" cellspacing="0" cellpadding="0">'+
					'<tr>'+
						'<td class="td_label">备　　注：</td>'+
						'<td><input class="comm_ibox" type="text" value="'+hz6d_coms_info[id].nick+'"/><input type="hidden" class="hidden_comm_ibox" value="'+hz6d_coms_info[id].nick+'"/></td>'+
					'</tr>'+
					'<tr>'+
						'<td class="td_label">分　　组：</td>'+
						'<td><select class="addFriend_sbox">'+option+'</select> <a class="color_blue addGroup">新建分组</a></td>'+
					'</tr>'+
				'</table>'+
			'</div>';
		}else if(type == 'addCompany'){
			title = '收藏企业';
			var option = '';
			for(var i in hz6d_fav_companys){
				if (hz6d_fav_companys.hasOwnProperty(i) && i!= 'tmpid'){
					option += '<option value="'+i+'">'+hz6d_fav_companys[i][0]+'</option>';
				}
			}
			content = '<div class="addFriendBox">'+
				'<table class="addFriendTable" border="0" cellspacing="0" cellpadding="0">'+
					'<tr>'+
						'<td class="td_label">备　　注：</td>'+
						'<td><input class="comm_ibox" type="text"/></td>'+
					'</tr>'+
					'<tr>'+
						'<td class="td_label">分　　组：</td>'+
						'<td><select class="addFriend_sbox">'+option+'</select> <a class="color_blue addGroup">新建分组</a></td>'+
					'</tr>'+
				'</table>'+
			'</div>';
		}
	}
	var html = '<div class="mnkf_mbox">'+
		'<div class="mnkf_mbox_bd">' +
			'<div class="mnkf_mbox_bg">' +
				'<div class="mnkf_mbox_head">' +
					'<div class="mnkf_mbox_title">'+title+'</div>' +
					'<a class="mnkf_mbox_close" onclick="hz6d_cancel();"></a>' +
				'</div>' +
				'<div class="mnkf_mbox_body">'+
					'<div style="padding:20px 20px 20px 20px;line-height:20px;">'+content+'</div>'+
				'</div>' +
				'<div class="mnkf_mbox_bottom">'+
					'<a class="greenBtn" onclick="hz6d_action(\''+type+'\','+id+','+gp_id+');">确定</a>　<a class="grayBtn" onclick="hz6d_cancel();">取消</a>&nbsp;&nbsp;'+
				'</div>'+
			'</div>' +
		'</div>'+
	'</div>';
	flp('body').append(html);
	flp('.mnkf_mbox').css('width','400px');
}
//弹出层的动作处理
function hz6d_action(type,id,gp_id){
	if(type == 'delCompany'){
		var opts = {'act':'DEL_COM','grpid':gp_id,comid:id};
		hz6d_sendKhFavData(opts, function(a){
			if(a == 1){
				flp('.mnkf_list_classify_create').prevAll('.mnkf_list_classify').remove();
				load_fav_companys();
			}else{
				hz6d_alert('您已删除该联系人，请点击确定重载好友');
				var opts = {'act':'GET'};
				hz6d_sendKhFavData(opts, function(a){
					if (a == 1) {
						HZ6D_CONFIGS.loadedfav = 1;
						load_fav_companys();
					}
				});
			}
			hz6d_cancel();
		});
		flp("ul.mnkf_content_head_icon li div.altbox").hide();
	}else if(type == 'delGroup'){
		var opts = {'act':'DEL_GRP','grpid':gp_id};
		hz6d_sendKhFavData(opts, function(a){
			if(a == 1){
				flp('.mnkf_list_classify_create').prevAll('.mnkf_list_classify').remove();
				load_fav_companys();
				flp('.mnkf_list_classify[id="fav_grp_' + gp_id + '"]').remove();
			}else{
				hz6d_alert("删除失败！");
			}
			hz6d_cancel();
		});
	}else if(type == 'delRecent'){
		var opts = {'act':'DEL_RCCT','comid':id};
		hz6d_sendKhFavData(opts, function(a){
			//if (a == 1) flp('.mnkf_list_scroll .mnkf_list_recent ul').find("#rcct_com_"+id).remove();
			//else alert('对不起，因为网络问题删除不成功');
		});
		flp('.mnkf_list_scroll .mnkf_list_recent ul').find("#rcct_com_"+id).remove();
		hz6d_cancel();
	}else if(type == 'editCompany'){
		var pre_nick = flp.trim(flp('.hidden_comm_ibox').val());
		var nick = flp.trim(flp('.comm_ibox').val());
		var group = flp('.addFriend_sbox').val();
		var i = 0;
		if(pre_nick!=nick && gp_id == group){
			var opts = {'act':'EDIT_COM','comnn':nick,'grpid':gp_id,comid:id};
			hz6d_sendKhFavData(opts, function(a){
				if(a == 1){
					flp('#hz6d_mnkh_list .mnkf_list_classify ul li[id="fav_com_'+id+'"]').find(".mnkf_nickname").html(nick);
					flp('#chat_com_'+id).find(".mnkf_nickname").html(nick);
					if (flp('#chat_com_'+id).hasClass('focus')) flp("div.l_title a.name").html(nick);
					load_recent_contact();
				}else{
					hz6d_alert("编辑失败！");
				}
				hz6d_cancel();
			});
		}
		else if(gp_id!=group){
			var opts = {'act':'MOVE_COM','grpid':group,comid:id};
			hz6d_sendKhFavData(opts, function(a){
				if(a == 1){
					i = i+1;
					flp('.mnkf_list_classify_create').prevAll('.mnkf_list_classify').remove();
					load_fav_companys();
					if (pre_nick !=nick){
						var opts = {'act':'EDIT_COM','comnn':nick,'grpid':group,comid:id};
						hz6d_sendKhFavData(opts, function(a){
							if(a == 1){
								flp('#hz6d_mnkh_list .mnkf_list_classify ul li[id="fav_com_'+id+'"]').find(".mnkf_nickname").html(nick);
								flp('#chat_com_'+id).find(".mnkf_nickname").html(nick);
								if (flp('#chat_com_'+id).hasClass('focus')) flp("div.l_title a.name").html(nick);
								load_recent_contact();
							}else{
								hz6d_alert("编辑失败！");
							}
						});
					}
				}else{
					i = i-1;
					hz6d_alert("编辑失败！");
				}
				hz6d_cancel();
			});
		}
	}else if(type == 'addCompany'){
		var nick = flp('.comm_ibox').val();
		var new_group = flp('.addFriend_sbox').val();
		var opts = {'act':'ADD_COM','comnm': hz6d_coms_info[id].name,'comnn':nick,'grpid':new_group,'comid':id};
		hz6d_sendKhFavData(opts, function(a, b){
			if(a == 1){
				var name = hz6d_coms_info[id].nick?hz6d_coms_info[id].nick:hz6d_coms_info[id].name;
				flp('.mnkf_list_classify[id="fav_grp_'+new_group+'"] ul').append('<li class="hz6d_kf" id="fav_com_'+id+'"><div class="avatar"><a><img style="height:20px;width:20px;" src="'+hz6d_coms_info[id].logo+'"></a><div class="status_icon s_'+hz6d_coms_info[id].status+'"></div></div><div class="mnkf_nickname">'+name+'</div><div class="btn_con" style="display:none;"><a class="edit"></a><a class="del"></a></div></li>');
				flp('#chat_com_'+id).find(".mnkf_nickname").html(name);
				if (flp('#chat_com_'+id).hasClass('focus')) flp("div.l_title a.name").html(name);
				var total = parseInt(flp('.mnkf_list_classify[id="fav_grp_'+new_group+'"]').find('em').html());
				total += 1;
				flp('.mnkf_list_classify[id="fav_grp_'+new_group+'"]').find('em').html(total);
				//load_fav_companys();
				load_recent_contact();
			}else{
				hz6d_alert('您已添加该联系人，请点击确定重载企业收藏夹');
				var opts = {'act':'GET'};
				hz6d_sendKhFavData(opts, function(a){
					if (a == 1) {
						HZ6D_CONFIGS.loadedfav = 1;
						load_fav_companys();
					}
				});
			}
			hz6d_cancel();
		});
		flp("ul.mnkf_content_head_icon li div.altbox").hide();
	}
}
//删除弹出层
function hz6d_cancel(box){
	if (box) {
		flp('.' + box).remove();
	} else {
		flp('.mnkf_mbox').remove();
	}
}
function init_div_minkh(com_id)
{
var hz6d_html = "";
hz6d_html += "<div id='hz6d_mnkh_talking' class='mnkf_talking' style='display: none;'>";
hz6d_html +=		"<div class='hz6d_bd newsbd'>";
hz6d_html +=			"<div class='icon_talking'></div>";
hz6d_html +=			"<div class='hz6d_t newChat'>&nbsp; &nbsp; &nbsp; 点击咨询</div>";
hz6d_html +=		"</div>";
hz6d_html += "</div>";

hz6d_html += "<div id='hz6d_mnkh_min' class='mnkf_min' style='display: none;'>";
hz6d_html +=		"<div class='hz6d_bd'>";
hz6d_html +=			"<div class='status_icon s_1'>1</div>";
hz6d_html +=			"<div class='hr'></div>";
hz6d_html +=			"<div class='hz6d_t'>企业收藏夹(0)</div>";
hz6d_html +=			"<a class='f_logo' title='Powered by 53KF' href='http://www.53kf.com/' target='_blank'></a>";
hz6d_html +=		"</div>";
hz6d_html += "</div>";

hz6d_html += "<div id='hz6d_mnkh_list' class='mnkf_list' style='display:none;'>";
hz6d_html +=	 "<div class='mnkf_searchbox' style='display:;'>";
hz6d_html +=		 "<div class='mnkf_list_recent'>";
hz6d_html +=			 "<ul>";
hz6d_html +=			 "</ul>";
hz6d_html +=		 "</div>";
hz6d_html +=	 "</div>";
hz6d_html +=		 "<div class='mnkf_list_head'>";
hz6d_html +=			 "<div class='hz6d_bd'>";
hz6d_html +=				 "<div class='status_change'>";
hz6d_html +=					 "<div class='ih'><div class='status_icon s_1'></div><span class='status_txt'>在线</span><span class='status_arrow'></span><span class='status_hr'></span></div>";
// hz6d_html +=					 "<div class='status_change_list'>";
// hz6d_html +=						 "<ul>";
// hz6d_html +=							 "<li><div class='status_icon s_1'></div><span>在线</span></li>";
// hz6d_html +=							 "<li><div class='status_icon s_2'></div><span>忙碌</span></li>";
// hz6d_html +=							 "<li><div class='status_icon s_3'></div><span>离开</span></li>";
// hz6d_html +=							 "<li><div class='status_icon s_4'></div><span>隐身</span></li>";
// hz6d_html +=						 "</ul>";
// hz6d_html +=					 "</div>";
hz6d_html +=				 "</div>";
hz6d_html +=				 "<div class='mnkf_list_conbtn'><a class='head_conbtn hcb_min'></a></div>";
hz6d_html +=			 "</div>";
hz6d_html +=		 "</div>";
hz6d_html +=		 "<div class='mnkf_search'>";
hz6d_html +=			 "<div class='mnkf_input_box'><input class='mnkf_search_txtbox' type='text' value='搜索' /><a class='mnkf_search_close'></a></div>";
hz6d_html +=		 "</div>";
hz6d_html +=		 "<div class='mnkf_list_body'>";
hz6d_html +=			 "<div class='mnkf_list_tag'>";
hz6d_html +=				 "<ul>";
hz6d_html +=					 "<li class='bd_none focus' for='mnkf_list1' title='企业收藏夹'><span class='icon_msg'></span></li>";
hz6d_html +=					 "<li for='mnkf_list1' title='最近联系'><span class='icon_history'></span></li>";
hz6d_html +=				 "</ul>";
hz6d_html +=			 "</div>";
hz6d_html +=			 "<div class='mnkf_list_scroll' id='icon_msg'>";
//hz6d_html +=				 '<div id="fav_grp_0" class="mnkf_list_classify"><div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>默认分组</span>(<em>0</em>)</div><div style="display: none;" class="btn_con"><a operating="edit" class="edit"></a></div></div><ul></ul></div>';
hz6d_html +=				 "<div class='mnkf_list_classify_create'><div class='icon_add'></div><div class='hz6d_t' style='cursor:pointer'>添加分组</div></div>";
hz6d_html +=			 "</div>";
hz6d_html +=			 "<div class='mnkf_list_scroll' style='display: none;' id='icon_history'>";
hz6d_html +=				 "<div class='mnkf_list_recent'>";
hz6d_html +=					 "<ul>";
hz6d_html +=					 "</ul>";
hz6d_html +=				 "</div>";
hz6d_html +=			 "</div>";
hz6d_html +=		 "</div>";
hz6d_html +=		 "<div class='mnkf_ad' style='width:175px;height:71px;'><iframe id='hz6d_iframe_logo' src='' scroll='no' frameborder='0' style='height:71px;width:175px;border:0'></iframe></div>";
hz6d_html +=		 "<div class='mnkf_list_f'><div class='minimize'><em></em></div><div class='hr'></div><a class='f_logo' title='Powered by 53KF' href='http://www.53kf.com/' target='_blank'></a></div>";
hz6d_html +=	 "</div>";


hz6d_html += "<div id='hz6d_mnkh_content' class='mnkf_content' style='display: none;'><div class='mnkf_content_head'>";
hz6d_html +=		"<div class='hz6d_bd'>";
//公司名片
hz6d_html +=			"<div class='l_title hz6d_clearfix'>";
hz6d_html +=				"<div class='status_icon' style='float:left'></div>";
hz6d_html +=				"<a class='l_title_t name' target='_blank' style='float:left'></a>";
hz6d_html +=				"<span class='certification' style='float:left'></span>";
hz6d_html +=			"</div>";
hz6d_html +=			"<ul class='mnkf_content_head_icon'>";
hz6d_html +=				"<li class='altbox_control icon_discription updown'>";
hz6d_html +=					"<div class='butbox' style='width:21px;height:21px;'></div>";
hz6d_html +=					"<div class='altbox' id='coms_info_div'>";
hz6d_html +=						"<div class='altbox_arrow'></div>";
hz6d_html +=						"<a class='altbox_close'></a>";
hz6d_html +=						"<div class='altbox_bg'>";
hz6d_html +=							"<div class='altbox_content altbox_company_info'>";
hz6d_html +=								"<div class='ci_box'>";
hz6d_html +=									"<a class='company_avatar'><img src=''></a>";
hz6d_html +=									"<a class='l_title_t name' target='_blank'></a>";
hz6d_html +=									"<span class='certification'></span>";
hz6d_html +=									"<p></p><p></p><p>网址：&nbsp;<a href='' style='color:#00f;' target='_blank'></a></p></div><div class='company_description'><div class='intro'></div><div><p class='com_attention' style='text-align:right;padding-right:15px;'></p></div>";
hz6d_html +=				"</div></div></div></div></li>";
//个人名片
hz6d_html +=				"<li class='altbox_control icon_contact updown'>";
hz6d_html +=					"<div class='butbox' style='width:21px;height:21px;'></div>";
hz6d_html +=					"<div class='altbox' id='workers_info_div'>";
hz6d_html +=						"<div class='altbox_arrow'></div>";
hz6d_html +=						"<a class='altbox_close'></a>";
hz6d_html +=						"<div class='altbox_bg'>";
hz6d_html +=							"<div class='altbox_content altbox_contact'>";
hz6d_html +=								"<table width='100%' border='0' cellpadding='0' cellspacing='0' class='workers_info'>";
hz6d_html +=									"<tr><td class='label'>昵称：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>电话：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>手机：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>邮箱：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>MSN：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>QQ：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>性别：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td class='label'>生日：</td><td class='content'></td></tr>";
hz6d_html +=									"<tr><td colspan='2' align='center'><a class='btn_reviews'><em></em>给Ta评分</a></td></tr>";
hz6d_html +=								"</table>";
hz6d_html +=				"</div></div></div></li>";
hz6d_html +=			"</ul>";
//对话框控制按钮
hz6d_html +=			"<div class='mnkf_content_conbtn'>";
hz6d_html +=				"<a class='head_conbtn hcb_min' onclick='hz6d_hideContent();'></a>";
hz6d_html +=				"<a class='head_conbtn hcb_close' onclick='hz6d_closeContent();'></a>";
hz6d_html +=			"</div>";
hz6d_html +=	"</div></div>";

//左侧聊天列表
hz6d_html += "<div class='mnkf_content_body'>";
hz6d_html +=		"<div class='mnkf_dialog_list'>";
hz6d_html +=			"<div class='mnkf_dialog_up'><em></em></div>";
hz6d_html +=			"<div class='mnkf_dialog_scroll'>";
hz6d_html +=				"<ul>";
// hz6d_html +=					"<li class='focus altbox_control out_altbox' id='chat_com_"+com_id+"'>";
// hz6d_html +=					"<a class='mnkf_dialog_list_close'></a>";
// hz6d_html +=					"<div class='avatar'><img src='"+HZ6D_CONFIGS.talk_host + "/minkh/style/53.jpg' name='" + com_id + "'>";
// hz6d_html +=						"<div class='status_icon'></div></div>";
// hz6d_html +=					"<div class='mnkf_nickname'></div>";
// hz6d_html +=					"<em class='arrow'></em></li>";
hz6d_html +=				"</ul>";
hz6d_html +=			"</div>";
hz6d_html +=			"<div class='mnkf_dialog_down'><em></em></div>";
hz6d_html +=		"</div>";
hz6d_html +=		"<div class='mnkf_dialog' id='hz6d_chatting_iframes'></div>";
hz6d_html += "</div></div>";

flp('body').append(hz6d_html);
}
//入口
flp(document).ready(function() {
	
	//hz6d_getCompanyInfo(HZ6D_CONFIGS.com_id);
	
	init_div_minkh(HZ6D_CONFIGS.com_id);
	var stay_altbox_timer = 0;
	var stay_obj = null;
	var hz6d_time_alt = 0;
	var closeStay = function() {
		window.clearTimeout(hz6d_time_alt);
		window.clearTimeout(stay_altbox_timer);
		flp("div.stay_altbox").remove();
	};

	//聊天列表
	flp('div.mnkf_dialog_scroll li').live('mouseenter',function(e){
		flp(this).find(".mnkf_dialog_list_close").show();
		if (stay_obj != flp(this)) {
			closeStay();
			var obj = flp(this);
			hz6d_time_alt = setTimeout(function(){mouse_chat_info(obj,e)},500);
		}
	});
	flp("div.mnkf_dialog_scroll li").live('mouseleave',function(){
		flp(this).find(".mnkf_dialog_list_close").hide();
		stay_obj = flp(this);
		window.clearTimeout(hz6d_time_alt);
		stay_altbox_timer = setTimeout(function() {
			//flp("div.stay_altbox").fadeOut('normal',function(){
				flp("div.stay_altbox").remove();
			//});
		}, 500);
	});
	//收藏夹 最近联系人
	flp('div.mnkf_list_scroll li div.avatar').live('mouseenter',function(e){
		if (stay_obj != flp(this)) {
			closeStay();
			var obj = flp(this).parent();
			hz6d_time_alt = setTimeout(function(){mouse_group_info(obj,e)},500);
		}
	});
	flp('div.mnkf_list_scroll li div.avatar').live('mouseleave',function(){
		stay_obj = flp(this);
		window.clearTimeout(hz6d_time_alt);
		stay_altbox_timer = setTimeout(function() {
			//flp("div.stay_altbox").fadeOut('normal',function(){
				flp("div.stay_altbox").remove();
			//});
		}, 500);
	});
	flp('div.stay_altbox').live('mouseenter',function(){
		window.clearTimeout(stay_altbox_timer);
	});
	flp('div.stay_altbox').live('mouseleave',function(){
		stay_altbox_timer = setTimeout(function() {
			//flp("div.stay_altbox").fadeOut('normal',function(){
				flp("div.stay_altbox").remove();
			//});
		}, 500);
	});

	hz6d_scroll();//聊天列表翻动
	flp("div.newsbd").click(function(){
		try{
			var cid = flp("div.mnkf_dialog_scroll li.focus").attr("id").replace('chat_com_','');
			hz6d_re_flashing(cid);
			hz6d_is_chatting();
		} catch(e){
		}
	});
	//显示状态列表
	flp('.status_change').hover(
		function(){
			flp(this).addClass('status_change_hover');
		},
		function(){
			flp(this).removeClass('status_change_hover');
		}
	);
	flp('.hz6d_cn,.mnkf_list_classify ul li,.mnkf_list_scroll .mnkf_list_recent ul li').live('mouseenter',function(){
		showMouseover(flp(this));
	});
	flp('.hz6d_cn,.mnkf_list_classify ul li,.mnkf_list_scroll .mnkf_list_recent ul li').live('mouseleave',function(){
		showMouseout(flp(this));
	});
	flp('.hz6d_cn').live('click',function(e){
		hz6d_showClick(flp(this),'group',flp(e.target).attr('class'));
	});
	flp('.mnkf_list_classify ul li').live('click',function(e){
		hz6d_showClick(flp(this),'company',flp(e.target).attr('class'));
	});
	flp('.mnkf_list_scroll .mnkf_list_recent ul li').live('click',function(e){
		hz6d_showClick(flp(this),'recent',flp(e.target).attr('class'));
	});
	flp('.hz6d_kf').live('dblclick',function(){
		try {
			if (flp.inArray(HZ6D_CONFIGS.comid, HZ6D_CONFIGS.chatting_comid) == -1) {
				add_chatting_list(HZ6D_CONFIGS.com_id,'add');
			}
			var com_id = flp(this).attr("id").replace('fav_com_','').replace('rcct_com_','');
			add_chatting_list(com_id,'add');
		} catch(e){}
	});
	//切换在线状态,需与对话环境进行交互
	flp('.status_change_list ul li').click(function(){
		var newclass = flp(this).children('div').attr('class');
		var newtxt = flp(this).children('span').html();
		flp('.ih').children('div').removeClass();
		flp('.ih').children('div').addClass(newclass);
		flp('.status_txt').html(newtxt);
	});
	//收藏夹，最近列表切换
	flp('.icon_msg').parent('li').click(function(){
		flp(this).addClass('focus');
		flp(this).siblings('li').removeClass('focus');
		flp('#icon_msg').css('display','');
		flp('#icon_history').css('display','none');
		flp('.mnkf_searchbox').css('display','none');
	});
	flp('.icon_history').parent('li').click(function(){
		flp(this).addClass('focus');
		flp(this).siblings('li').removeClass('focus');
		flp('#icon_history').css('display','');
		flp('#icon_msg').css('display','none');
		flp('.mnkf_searchbox').css('display','none');
	});
	//右上角，收藏夹最小化
	flp('.mnkf_list_conbtn').click(function(){
		flp('.mnkf_list').hide();
		flp('.mnkf_min').show();
	});
	//右下角，收藏夹最小化，需正确显示聊天人数
	flp('.minimize').click(function(){
		flp('.mnkf_list').hide();
		flp('.mnkf_min').show();
		//var num = flp('.mnkf_dialog_scroll ul li').length;
		//flp('.mnkf_min').find('.hz6d_t').html('聊天 ('+num+')');
	});
	//右下角，收藏夹最大化
	flp('.mnkf_min').click(function(){
		if (!HZ6D_CONFIGS.guest_id) {
			hz6d_showContent();
		}
		flp('.mnkf_min').hide();
		flp('.mnkf_list').show();
	});
	//显示聊天窗口
	flp('#hz6d_mnkh_talking').click(function(){
		hz6d_showContent();
	});
	//创建新分类，输入层
	flp('.mnkf_list_classify_create').click(function(){
		if (HZ6D_CONFIGS.guest_id != '' && HZ6D_CONFIGS.guest_id != 0 && HZ6D_CONFIGS.loadedfav == 1) {
			flp(this).before('<div class="mnkf_list_classify"><div class="hz6d_cn ishow"><div class="arrow"></div><input class="createbox" type="text"/></div><ul></ul></div>');
			flp('.createbox').focus();
		}
		else if (HZ6D_CONFIGS.guest_id == '' || HZ6D_CONFIGS.guest_id == 0){
			hz6d_alert('正在建立对话，请稍候添加分组');
		}
		else{
			hz6d_alert('网络修复中，请稍候添加分组');
		}
	});
	//失焦或者按enter键
	flp('.createbox').live('blur keydown',function(e){
		if((e.type=='focusout')||(e.type=='keydown'&&e.keyCode==13)){
			if(flp.trim(flp(this).val()) == ''){
				flp(this).parents('.mnkf_list_classify').remove();
			}else{
				var that = flp(this);
				var gn = flp.trim(flp(this).val());
				for(var i in hz6d_fav_companys){
					if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
					if(gn == hz6d_fav_companys[i][0]){
						hz6d_alert("此分组名已存在！");
						flp(this).focus();
						return false;
					}
				}
				var opts = {'act':'ADD_GRP','grpnm':gn};
				hz6d_sendKhFavData(opts, function(a,b){
					if(a == 1){
						var pa = that.parents('.mnkf_list_classify');
						pa.html('<div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>'+gn+'</span>(<em>0</em>)</div><div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a><a class="del" operating="del"></a></div></div><ul></ul>');
						pa.attr("id",'fav_grp_' + b);
					}else{
						hz6d_alert("添加失败！");
					}
				});
			}
		}
	});
	//编辑组名
	flp('.editing').live('blur keydown',function(e){
			var new_gp = flp.trim(flp(this).val());
			var gp_id = -1;
			var that = flp(this);
			gp_id = flp(this).parents('.mnkf_list_classify').attr("id").replace('fav_grp_','');
			if(typeof(gp_id) == 'undefined') gp_id = flp(this).parents('div[class="mnkf_list_classify mnkf_list_classify_show"]').attr("id").replace('fav_grp_','');
			if((e.type=='focusout')||(e.type=='keydown'&&e.keyCode==13)){
				for(var i in hz6d_fav_companys){
					if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
					if(new_gp == hz6d_fav_companys[i][0] && gp_id != i && i != 'tmpid'){
						hz6d_alert("此分组名已存在！");
						flp(this).focus();
						return false;
					}
				}
				var opts = {'act':'EDIT_GRP','grpnm':new_gp,'grpid':gp_id};
				hz6d_sendKhFavData(opts, function(a){
					if(a == 1){
						that.parent('span').html(that.val());
					}else{
						hz6d_alert("修改组名失败！");
					}
				});
			}
			if(gp_id == 0){
				flp(this).parents('.hz6d_cn').append('<div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a></div>');
			}else{
				flp(this).parents('.hz6d_cn').append('<div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a><a class="del"></a></div>');
			}
	});
	var mnkf_search_txtbox_blured = 0,
		is_on_mnkf_searchbox = 0,
		mnkf_searchbox_slideUp_timer = 0;
	//搜索功能
	//聚焦
	flp('.mnkf_search_txtbox').focus(function(){
		clearTimeout(mnkf_searchbox_slideUp_timer);
		if(flp(this).val() == '搜索' || flp(this).val() == ''){
			flp(this).val('');
		}else{
			flp(".mnkf_searchbox ul li[class='hz6d_kf " + hz6d_divItemSelect + "']").removeClass(hz6d_divItemSelect);
			flp('.mnkf_searchbox').slideDown(200);
		}
		mnkf_search_txtbox_blured = 0;
	});

	//失焦
	flp('.mnkf_search_txtbox').blur(function(e){
		if(flp.trim(flp(this).val())==''){
			flp(this).val('搜索');
			flp('.mnkf_searchbox .mnkf_list_recent ul').html('');
		}else{
			mnkf_searchbox_slideUp_timer = setTimeout(function(){
				if(!is_on_mnkf_searchbox) flp('.mnkf_searchbox').slideUp(800);
			},1000);
		}
		mnkf_search_txtbox_blured = 1;
	});
	flp('.mnkf_searchbox').mouseleave(function(){
		if(mnkf_search_txtbox_blured) flp(this).slideUp(800);
		is_on_mnkf_searchbox = 0;
	});
	flp('.mnkf_searchbox').mouseenter(function(){
		is_on_mnkf_searchbox = 1;
	});
	
	flp('.mnkf_search_close').click(function(){
		flp('.mnkf_search_txtbox').val('');
		flp('.mnkf_search_txtbox').focus();
		flp('.mnkf_searchbox').slideUp(100);
	});
	//主要动作
	flp('.mnkf_search_txtbox').keyup(function(event){
		if(event.which == 40) {//down
			chageSelect(1);
			//alert(event.which)
		}else if (event.which == 38) {//up
			//alert(event.which)
			chageSelect(-1);
		}else if (event.which == 13) {//回车
			var obj = flp(".mnkf_searchbox ul li[class='hz6d_kf " + hz6d_divItemSelect + "']");
			if(obj.index() == -1) return false;
			try{
				add_chatting_list(obj.attr("id").replace('fav_com_','').replace('rcct_com_',''),'add');
			} catch(e){}
			flp('.mnkf_searchbox').slideUp(200);
		}else if (this.value.length > 0) {
			var count = 0;
			var str = flp.trim(this.value);
			flp('.mnkf_searchbox .mnkf_list_recent ul').html('');
			var arr = [];
			for(var i in hz6d_fav_companys){
				if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
				for (var j = 0, len = hz6d_fav_companys[i][1].length; j < len; j++) {
					arr.push(hz6d_fav_companys[i][1][j]);
				}
			}
			for (var i = 0, len = hz6d_recentContact.length; i < len; i++) {
				if (arr.indexOf(hz6d_recentContact[i]) == -1) arr.push(hz6d_recentContact[i]);
			}
			for(var j = 0, len = arr.length; j < len; j++){
				for(var i in hz6d_coms_info){
					if (!hz6d_coms_info.hasOwnProperty(i)) continue;
					if(arr[j] == i){
						if(hz6d_coms_info[i].nick.indexOf(str)!=-1 || hz6d_coms_info[i].name.indexOf(str)!=-1){
							var name = hz6d_coms_info[i].nick ? hz6d_coms_info[i].nick : hz6d_coms_info[i].name;
							count = count+1;
							flp('.mnkf_searchbox .mnkf_list_recent ul').append('<li class="hz6d_kf" id="rcct_com_'+i+'"><div class="avatar"><a><img style="height:20px;width:20px;" src="'+hz6d_coms_info[i].logo+'" /></a><div class="status_icon s_'+hz6d_coms_info[i].status+'"></div></div><div class="mnkf_nickname">'+name+'</div><div class="btn_con" style="display:none;"></div></li>');
							//flp('.mnkf_searchbox .mnkf_list_recent ul').append('<li class="hz6d_kf" id="rcct_com_'+i+'"><div class="avatar"><a href=""><img style="height:20px;width:20px;" src="'+hz6d_coms_info[i].logo+'"></a><div class="status_icon s_'+hz6d_coms_info[i].status+'"></div></div><div class="mnkf_nickname">'+name+'</div></div><input type="hidden" name="d_index" value="'+(count-1)+'"/></li>');
						}
					}
				}
			}
			if(count == 0){
				flp('.mnkf_searchbox .mnkf_list_recent ul').append('<li class="hz6d_kf"><div class="mnkf_nickname">无相关记录<div></li>');
			} else{
				flp(".mnkf_searchbox ul li:eq(0)").addClass(hz6d_divItemSelect);
			}
			flp('.mnkf_searchbox').slideDown(200);
		}else {
			flp('.mnkf_searchbox').slideUp(200);
		}
	});
	//消除用键盘选中和用鼠标选中同时作用的bug
	flp('.mnkf_searchbox .mnkf_list_recent ul li').live('mouseover',function(){
		flp(".mnkf_searchbox ul li[class='hz6d_kf " + hz6d_divItemSelect + "']").removeClass(hz6d_divItemSelect);
		flp(this).addClass('div_item_select');
	});
	flp('.mnkf_searchbox .mnkf_list_recent ul li').live('mouseout',function(){
		flp(this).removeClass('div_item_select');
	});
	//双击搜索到的联系人
	flp('.mnkf_searchbox .mnkf_list_recent ul li').live('dblclick',function(){
		try{
			var id = flp(this).attr("id").replace('fav_com_','').replace('rcct_com_','');
			add_chatting_list(id,'add');
		} catch(e){}
		flp('.mnkf_searchbox').slideUp(200);
	});
	//在弹出层中新建分组
	flp('a[class="color_blue addGroup"]').live('click',function(){
		flp(this).parents('.addFriendTable').append('<tr><td class="td_label">新建分组：</td><td><input class="comm_ibox" type="text"> <a class="grayBtn newGroup">创建</a> <a class="color_blue cancelGroup">取消</a></td></tr>');
		flp('.comm_ibox').focus();
	});
	//在弹出层中取消新建分组
	flp('a[class="color_blue cancelGroup"]').live('click',function(){
		flp(this).parents('tr').remove();
	});
	//在弹出层中新建分组的动作
	flp('a[class="grayBtn newGroup"]').live('click',function(){
		var newGroup = flp(this).prev('.comm_ibox').val();
		newGroup = flp.trim(newGroup);
		if(newGroup != ''){
			var that = flp(this);
			for(var i in hz6d_fav_companys){
				if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
				if(newGroup == hz6d_fav_companys[i][0]){
					hz6d_alert("此分组名已存在！");
					return false;
				}
			}
			var opts = {'act':'ADD_GRP','grpnm':newGroup};
			hz6d_sendKhFavData(opts, function(a, b){
				if(a == 1){
					that.parents('tr').prev('tr').find('.addFriend_sbox').append('<option value="'+ b +'" selected>'+newGroup+'</option>');
					that.parents('tr').remove();
					flp('.mnkf_list_classify_create').before('<div class="mnkf_list_classify" id="fav_grp_'+ b +'"><div class="hz6d_cn"><div class="arrow"></div><div class="hz6d_t"><span>'+newGroup+'</span>(<em>0</em>)</div><div class="btn_con" style="display:none;"><a class="edit" operating="edit"></a><a class="del"></a></div></div><ul></ul></div>');
				}else{
					hz6d_alert("添加分组失败！");
				}
			});
		}else{
			hz6d_alert("分组名不能为空！");
			flp(this).prev('.comm_ibox').focus();
		}
	});

	/****************************************************/

	/////////////fengliu//////////////
	// 控制开关弹出层，默认关闭弹出层。
	flp("ul.mnkf_content_head_icon li div.altbox").hide();
	flp("ul.mnkf_content_head_icon li div.butbox").click(function(){
		if(flp(this).parent().find("div.altbox").is(":hidden")){

			var cid = flp("#hz6d_mnkh_content .mnkf_dialog_scroll li.focus").attr("id").replace('chat_com_','');
			if(!cid){
				cid = flp("#hz6d_mnkh_content div.mnkf_dialog_scroll li:last").attr("id").replace('chat_com_','');
			}
			if(flp(this).parent().index() == 0){
				dis_com_card(cid, 'click');
			}else{
				dis_worker_card(cid);
			}

		} else {
			flp(this).parent().find("div.altbox").hide();
		}
	});
	// 关闭弹出层
	flp("div.altbox a.altbox_close").click(function(){
		flp(this).parent().hide();
	});
	//左侧正在聊天列表切换
	flp("div.mnkf_dialog_scroll li").live('click',function(){
		var cid = flp(this).attr("id").replace('chat_com_','');
		flp(this).siblings("li").removeClass("focus");
		flp(this).addClass("focus");
		try {
			var iframes = hz6d_ID('hz6d_chatting_iframes').getElementsByTagName('iframe');
			for (var i = 0; i < iframes.length; i++) {
				iframes[i].style.display = 'none';
			}
			hz6d_ID('hz6d_chat_iframe_' + cid).style.display = 'block';
		} catch(e){}
		
		dis_com_card(cid);
		if(!flp('#workers_info_div').is(':hidden')) dis_worker_card(cid);
		hz6d_re_flashing(cid);
		hz6d_is_chatting();
	});
	//删除聊天人
	flp("div.mnkf_dialog_scroll a.mnkf_dialog_list_close").live('click',function(){
		var com_id = flp(this).parent().attr('id').replace('chat_com_','');
		var tmp_comfirm = '是否关闭该对话?';
		if (hz6d_coms_info[com_id].linked == '1') tmp_comfirm = '是否关闭该对话?\n请您对我的服务给予评价!';
		hz6d_confirm(tmp_comfirm, 'hz6d_close_chat_confirm', com_id);
		return false;
	});

	 var get_fav_company_timer = setInterval(function(){
		if (HZ6D_CONFIGS.guest_id != '' && HZ6D_CONFIGS.guest_id != 0 && HZ6D_CONFIGS.guest_id != undefined) {
			var opts = {'act':'GET'};
			hz6d_sendKhFavData(opts, function(a){
				if (a == 1) {
					HZ6D_CONFIGS.loadedfav = 1;
					load_fav_companys();
					for (var i in hz6d_fav_companys){
						if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
						for (var j = 0, len = hz6d_fav_companys[i][1].length; j < len; j++) {
							com_is_certified(hz6d_fav_companys[i][1][j]);
						}
					}
				}
				//else alert("加载收藏夹失败！");
			});
			clearInterval(get_fav_company_timer);
		}
	},500);

	var get_recent_contact_timer = setInterval(function(){
		if (HZ6D_CONFIGS.guest_id != '' && HZ6D_CONFIGS.guest_id != 0 && HZ6D_CONFIGS.guest_id != undefined) {
			hz6d_sendKhFavData({act:'RCCT'}, function(a){
				if (a == 1) load_recent_contact();
				//else alert("加载最近联系人失败！");
			});
			clearInterval(get_recent_contact_timer);
		}
	},500);
	setInterval(function(){
		recvDataFromIframeProxy();
	},100); 

	setTimeout(function(){
		flp("#hz6d_mnkh_talking,#hz6d_mnkh_min").show();
		load_fav_companys();
	},2000);
	// 阻止没有设置网址的企业名片
	flp("[href='--']").live('click',function(e){
		e.preventDefault();
	});
});


function hz6d_close_chat_confirm(com_id){
	if (hz6d_coms_info[com_id].vote == '1' && hz6d_coms_info[com_id].linked == '1'){
		sendDataToIframeProxy(com_id, 'vote', String(Math.random()));
	}
	var tmp_timer = setInterval(function(){
		if (hz6d_coms_info[com_id].voted == '1' || hz6d_coms_info[com_id].linked != '1') {
			clearInterval(tmp_timer);
			flp('#chat_com_' + com_id).remove();
			flp("div.mnkf_dialog_scroll li:first").addClass("focus");
			try { var cid = flp("div.mnkf_dialog_scroll li:first").attr("id").replace('chat_com_','');} catch(e){}
			var _tmp = 1;
			for (var i in hz6d_fav_companys){
				if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
				for (var j = 0, len = hz6d_fav_companys[i][1].length; j < len; j++) {
					if (hz6d_fav_companys[i][1][j] == com_id) {
						_tmp = 0;
						break;
					}
				}
			}
			if (_tmp == 1 && HZ6D_CONFIGS.guest_id != '' && HZ6D_CONFIGS.guest_id != 0 && HZ6D_CONFIGS.loadedfav == 1) {
				hz6d_showMsg(com_id,"addCompany");
			}
			//关闭对话iframe

			close_chatting_iframe(com_id);
			HZ6D_CONFIGS.chatting_comid.splice(flp.inArray(com_id,HZ6D_CONFIGS.chatting_comid),1);

			if(!cid){//若无聊天人则关闭聊天窗口，否则更改对应公司等数据
				var num = flp('.mnkf_dialog_scroll ul li').length;
				//flp('.mnkf_min').find('.hz6d_t').html('聊天 ('+num+')');
				flp("div.mnkf_content").hide();
				HZ6D_CONFIGS.firstopen = 1;
				flp("div.mnkf_talking").show();
				flp('div.hz6d_bd div.newChat').html("&nbsp; &nbsp; &nbsp; 点击咨询");
			}else{
				flp("#hz6d_chat_iframe_"+cid).css("display","block");
				//取消闪烁
				hz6d_re_flashing(cid);
				hz6d_is_chatting();
				dis_com_card(cid);
				if(!flp('#workers_info_div').is(':hidden')) dis_worker_card(cid);
			}
		}
	},1000);
}


//加载公司信息
function dis_com_card(com_id,type){
	var tmp_hidden = flp('#coms_info_div').is(':hidden');
	flp("ul.mnkf_content_head_icon li:eq(0)").find("div.altbox").hide();
	flp("ul.mnkf_content_head_icon li:eq(1)").find("div.altbox").hide();
	hz6d_getCompanyInfo(com_id, function(a, com_id){
		if (a == 1) {
			var gp_id = get_gp_id(com_id);
			flp(".l_title div.status_icon").removeClass("s_4");
			flp(".l_title div.status_icon").removeClass("s_1");
			flp(".l_title div.status_icon").addClass("s_" + hz6d_coms_info[com_id].status);
			flp("a.name").html(hz6d_coms_info[com_id].nick || hz6d_coms_info[com_id].name);
			flp("div.ci_box a img").attr("src",hz6d_coms_info[com_id].logo);
			flp("div.ci_box p:eq(0)").html(hz6d_add_shaft(hz6d_coms_info[com_id].addr));
			flp("div.ci_box p:eq(1)").html("电话： "+hz6d_add_shaft(hz6d_coms_info[com_id].tel));
			flp("div.ci_box p:eq(2) a").html(hz6d_add_shaft(hz6d_coms_info[com_id].site.replace('http://','')));
			// if(hz6d_coms_info[com_id].site.indexOf("http://") == -1){
				// flp("div.ci_box p:eq(2) a").attr("href","http://"+hz6d_coms_info[com_id].site);
				// flp("a.l_title_t").attr("href","http://"+hz6d_coms_info[com_id].site);
			// }else{
				flp("div.ci_box p:eq(2) a").attr("href",hz6d_coms_info[com_id].site);
				flp("a.l_title_t").attr("href",hz6d_coms_info[com_id].site);
			//}
			flp("div.intro").html(flp.trim(hz6d_coms_info[com_id].intro));
			flp("div.intro").attr("title",hz6d_coms_info[com_id].intro.replace(/<\/?[^>]*>/g,'').replace(/&nbsp;/ig,'').replace(/ /g,''));
			//是否验证图标初始化
			flp("span.certification").removeClass("icon_approve");
			flp("span.certification").removeAttr("title");
			if(hz6d_coms_info[com_id].cert == 1){
				flp("span.certification").addClass("icon_approve");
				flp("span.certification").attr("title",'企业身份认证');
			}
			//flp("ul.mnkf_content_head_icon li:eq(0)").find("div.altbox").show();
			var _tmp = 1;
			for(var i in hz6d_fav_companys){
				if (!hz6d_fav_companys.hasOwnProperty(i) || i == 'tmpid') continue;
				for (var j = 0, len = hz6d_fav_companys[i][1].length; j < len; j++) {
					if(hz6d_fav_companys[i][1][j] == com_id){
						_tmp = 0;
						break;
					}
				}
			}
			if (_tmp == 1) {
				flp("p.com_attention").html("<a class='btn_follow' onclick='hz6d_showMsg("+com_id+",\"addCompany\","+gp_id+");flp(\"ul.mnkf_content_head_icon li:eq(0)\").find(\"div.altbox\").hide();'><em></em>收藏该企业</a>");
			} else {
				flp("p.com_attention").html("<a class='btn_follow' onclick='hz6d_showMsg("+com_id+",\"delCompany\","+gp_id+");flp(\"ul.mnkf_content_head_icon li:eq(0)\").find(\"div.altbox\").hide();'><em></em>取消收藏</a>");
			}
			if(type == 'click' || !tmp_hidden) flp("ul.mnkf_content_head_icon li:eq(0)").find("div.altbox").show();

			var altbox_control = flp("ul.mnkf_content_head_icon li:eq(0)");
			var altbox = altbox_control.find('.altbox');
			var updown = altbox_control.hasClass('updown');
			if (updown) {
				var A = document,
						B = A.compatMode == "BackCompat" ? A.body: A.documentElement;
				var myClientHeight = B.clientHeight;
				var altHeight = altbox.outerHeight(true);
				(myClientHeight - 397 - 190 < 0) ? altbox.addClass('altbox_bottom') : altbox.removeClass('altbox_bottom');
			}
		}
	});
}


//加载客服员工信息
function dis_worker_card(com_id){
	flp("ul.mnkf_content_head_icon li:eq(0)").find("div.altbox").hide();
	flp("ul.mnkf_content_head_icon li:eq(1)").find("div.altbox").hide();
	try{
		if (typeof hz6d_workers_info[com_id]['id'] == 'undefined' || hz6d_workers_info[com_id]['id'] == ''){
			flp('#workers_info_div').css('display','none');
		}
		if (!hz6d_workers_info[com_id]['id'] || !hz6d_coms_info[com_id].kfcard || hz6d_coms_info[com_id].kfcard == 0) return;
	} catch(e){}
	hz6d_getWorkerInfo(com_id, function(a, com_id){
		if(!!hz6d_workers_info[com_id] && a == 1) {
			flp("table.workers_info tr td:eq(1)").html(hz6d_add_shaft(hz6d_workers_info[com_id].name));
			flp("table.workers_info tr td:eq(3)").html(hz6d_add_shaft(hz6d_workers_info[com_id].tel));
			flp("table.workers_info tr td:eq(5)").html(hz6d_add_shaft(hz6d_workers_info[com_id].mobi));
			flp("table.workers_info tr td:eq(7)").html(hz6d_add_shaft(hz6d_workers_info[com_id].email));
			flp("table.workers_info tr td:eq(9)").html(hz6d_add_shaft(hz6d_workers_info[com_id].msn));
			flp("table.workers_info tr td:eq(11)").html(hz6d_add_shaft(hz6d_workers_info[com_id].qq));
			flp("table.workers_info tr td:eq(13)").html(hz6d_add_shaft(hz6d_workers_info[com_id].sex == '1' ? '男' : '女'));
			flp("table.workers_info tr td:eq(15)").html(hz6d_add_shaft(hz6d_workers_info[com_id].birth));
			
			if (hz6d_workers_info[com_id].email != '') flp("table.workers_info tr td:eq(7)").attr('title',hz6d_workers_info[com_id].email);
			if (hz6d_workers_info[com_id].msn != '') flp("table.workers_info tr td:eq(9)").attr('title',hz6d_workers_info[com_id].msn);
			if (hz6d_coms_info[com_id].vote == '1') {
				flp("table.workers_info tr a.btn_reviews").parent().show();
				flp("table.workers_info tr a.btn_reviews").click(function(){
					sendDataToIframeProxy(com_id, 'vote', String(Math.random()));
					flp('#workers_info_div').hide();
					flp('#coms_info_div').hide();
				});
			} else {
				flp("table.workers_info tr a.btn_reviews").parent().hide();
			}
			flp('#workers_info_div').show();
			var altbox_control = flp("ul.mnkf_content_head_icon li:eq(1)");
			var altbox = altbox_control.find('.altbox');
			var updown = altbox_control.hasClass('updown');
			if (updown) {
				var A = document,
						B = A.compatMode == "BackCompat" ? A.body: A.documentElement;
				var myClientHeight = B.clientHeight;
				var altHeight = altbox.outerHeight(true);
				if (altHeight == 0) altHeight = 210;
				(myClientHeight - 397 - altHeight < 0 ) ? altbox.addClass('altbox_bottom') : altbox.removeClass('altbox_bottom');
			}
		}
	});
}

//显示左侧正在聊天列表
function dis_chatting_list(com_id){
	hz6d_getCompanyInfo(com_id, function(a, com_id){
		if (a == 1) {
			//在线状态
			var status_icon = flp("div.mnkf_dialog_scroll div.status_icon:last,div.l_title div.status_icon");
			if(hz6d_coms_info[com_id].status == "1"){
				status_icon.addClass("s_1");
			}else if(hz6d_coms_info[com_id].status == "2"){
				status_icon.addClass("s_2");
			}else if(hz6d_coms_info[com_id].status == "3"){
				status_icon.addClass("s_3");
			}else{
				status_icon.addClass("s_4");
			}
			//公司备注若无则显示公司名称
			if(hz6d_coms_info[com_id].nick != ""){
				flp("div.mnkf_dialog_scroll div.mnkf_nickname:last").html(hz6d_coms_info[com_id].nick);
			}else{
				flp("div.mnkf_dialog_scroll div.mnkf_nickname:last").html(hz6d_coms_info[com_id].name);
			}
		}
	});
}

//信息为空时输出"--"
function hz6d_add_shaft(data){
	if(data == ""){
		data = "--";
	}
	return data;
}

//取消闪烁
function hz6d_re_flashing(cid){
	clearInterval(HZ6D_CONFIGS.flashingInterval[cid]);
	clearInterval(HZ6D_CONFIGS.newMsgInterval);
	delete HZ6D_CONFIGS.flashingInterval[cid];
	HZ6D_CONFIGS.newMsgInterval = 0;
}

//判断是否有新聊天消息
function hz6d_is_chatting(){
	var intervalLength = 0;

	for(var i in HZ6D_CONFIGS.flashingInterval){
		if (HZ6D_CONFIGS.flashingInterval.hasOwnProperty(i)) intervalLength++;
	}
	if(intervalLength == 0){//无新聊天消息
		var cid = flp("div.mnkf_dialog_scroll ul li.focus").attr("id").replace('chat_com_','');
		if(!!cid && !!hz6d_coms_info[cid]){//缩小聊天框时仍有正在聊天人
			if(hz6d_coms_info[cid].nick != ""){
				flp('div.hz6d_bd div.newChat').html("&nbsp; 正与"+hz6d_coms_info[cid].nick+" 聊天中");
			}else{
				flp('div.hz6d_bd div.newChat').html("&nbsp; 正与"+hz6d_coms_info[cid].name+" 聊天中");
			}
		}else{
			flp('div.hz6d_bd div.newChat').html("&nbsp; &nbsp; &nbsp; 点击咨询");
		}
	}
}


//鼠标停留弹出公司信息
function mouse_chat_info(obj,e){
	var cid = obj.attr("id").replace('rcct_com_','').replace('fav_com_','').replace('chat_com_','');
	hz6d_alt_info(cid, function(altinfo){
		var content = obj.parents('.mnkf_content');
		content.append(altinfo);
		var contentOffset = content.offset();
		var offset = obj.offset();
		var top = offset.top - contentOffset.top;
		flp("div.stay_altbox").css("top", top +"px");
		flp("div.stay_altbox").css('right', '444px');
		//flp("div.stay_altbox").css({"top": top +"px", 'right': '444px'});
		//flp("div.stay_altbox").css({width:"270px",height:"180px"});
		flp("div.stay_altbox").css("width", "270px");
		flp("div.stay_altbox").css('height', "180px");
		//flp("div.stay_altbox").animate({width:"270px",height:"180px"});
		if ((flp("div.stay_altbox").outerHeight(true) + top) > 397) flp("div.stay_altbox").css("top", (397- flp("div.stay_altbox").outerHeight(true))+"px");
	});
}

function mouse_group_info(obj,e){
	var cid = obj.attr("id").replace('rcct_com_','').replace('fav_com_','').replace('chat_com_','');
	hz6d_alt_info(cid, function(altinfo){
		var list = obj.parents('.mnkf_list');
		list.append(altinfo);
		var listOffset = list.offset();
		var offset = obj.offset();
		var top = offset.top - listOffset.top;
		flp("div.stay_altbox").css("top",top+"px");
		flp("div.stay_altbox").css("width", "270px");
		flp("div.stay_altbox").css('height', "180px");
		//flp("div.stay_altbox").animate({width:"270px",height:"180px"});
		if ((flp("div.stay_altbox").outerHeight(true) + top) > 397) flp("div.stay_altbox").css("top", (397- flp("div.stay_altbox").outerHeight(true))+"px");
	});
}

function hz6d_alt_info(cid, callback){
	var altinfo = '';
	hz6d_getCompanyInfo(cid, function(a, cid){
		if (a == 1) {
			altinfo = "<div class='stay_altbox out_altbox' style='width:0;height:0'>";
			altinfo +=	 "<div class='altbox_bg'>";
			altinfo +=		 "<div class='altbox_content altbox_company_info' style='_height:1%;'>";
			altinfo +=			 "<div class='ci_box'>";
			altinfo +=				 "<a class='company_avatar'><img src='"+HZ6D_CONFIGS.talk_host + "/minkh/style/53.jpg' src='"+hz6d_coms_info[cid].logo+"'></a><a class='l_title_t' target='_blank' href='" + hz6d_coms_info[cid].site + "' style='color:#1481d3;'>"+(hz6d_coms_info[cid].nick || hz6d_coms_info[cid].name)+"</a>";
			altinfo +=				 "<span title='" + (hz6d_coms_info[cid].cert == '1' ? '企业身份认证' : '') + "' class='certification " + (hz6d_coms_info[cid].cert == '1' ? 'icon_approve' : '') + "'></span>";
			altinfo +=				 "<p>"+((hz6d_coms_info[cid].addr == '') ? '--' : hz6d_coms_info[cid].addr) +"</p>"
			altinfo +=				 "<p>电话："+hz6d_coms_info[cid].tel+"</p>";
			altinfo +=				 "<p>网址：<a href='" + hz6d_coms_info[cid].site + "' style='color:#00f;' target='_blank'>"+hz6d_coms_info[cid].site.replace('http://','')+"</a></p>";
			altinfo +=			 "</div>";
			altinfo +=			 "<div class='company_description' style='margin: 15px 10px 8px; height: 65px; width:238px; overflow: hidden;' title='" + hz6d_coms_info[cid].intro.replace(/<\/?[^>]*>/g,'').replace(/&nbsp;/ig,'').replace(/ /g,'')	+ "'>"+flp.trim(hz6d_coms_info[cid].intro) +"</div>";
			altinfo +=		 "</div>";
			altinfo +=	 "</div>";
			altinfo += "</div>";
			callback(altinfo);
		}
	});
}

//聊天列表上下翻动
function hz6d_scroll(){
	//当能够上下拉动时添加样式
	flp("div.mnkf_dialog_up,div.mnkf_dialog_down").hover(function(){
		flp(this).addClass("btn_bg");
	},function(){
		flp(this).removeClass("btn_bg");
	});
	flp("div.mnkf_dialog_up").bind('click',hz6d_scrollUp);
	flp("div.mnkf_dialog_down").bind('click',hz6d_scrollDown);
}
function hz6d_scrollUp(){
	var _this = flp("div.mnkf_dialog_scroll ul");
	var chat_count = _this.find("li").length;
	if(!flp("div.mnkf_dialog_scroll ul").is(":animated")){
		if(chat_count > hz6d_line){
			hz6d_line++;
			_this.animate({ marginTop: "-=" + 30 + "px" }, 300);
		}
	}
}
function hz6d_scrollDown(){
	var _this = flp("div.mnkf_dialog_scroll ul");
	var chat_count = _this.find("li").length;
	if(!flp("div.mnkf_dialog_scroll ul").is(":animated")){
		if(hz6d_line > 11){
			hz6d_line--;
			_this.animate({ marginTop: "+=" + 30 + "px" }, 300);
		}
	}
}

//添加到聊天列表
function add_chatting_list(com_id,type,kf){
	//创建对话窗口
	hz6d_getCompanyInfo(com_id, function(a, com_id){
		if (a == 1) {
			if (flp.inArray(com_id,HZ6D_CONFIGS.chatting_comid) == -1) HZ6D_CONFIGS.chatting_comid.push(com_id);
			var arr_com_id = [];
			flp("div.mnkf_dialog_scroll li").each(function(){arr_com_id.push(flp(this).attr("id").replace('chat_com_',''));});
			if(flp.inArray(com_id,arr_com_id) == -1){//判断是否已存在聊天
				if(type == "add"){
					//hz6d_showContent();
					flp("div.mnkf_list").show();
					flp("div.mnkf_content").show();
					flp("div.mnkf_talking").hide();
					flp("div.mnkf_dialog_scroll li").removeClass("focus");
					flp("div.mnkf_dialog_scroll ul").append("<li class='focus out_altbox' id='chat_com_"+com_id+"'><a class='mnkf_dialog_list_close'></a><div class='avatar'><img src='"+hz6d_coms_info[com_id].logo+"' name="+com_id+"><div class='status_icon'></div></div><div class='mnkf_nickname'></div><em class='arrow'></em></li>");
					dis_com_card(com_id);
					if(!flp('#workers_info_div').is(':hidden')) dis_worker_card(com_id);
				}else if(type == "flashing"){
					flp("div.mnkf_dialog_scroll ul").append("<li class='out_altbox' id='chat_com_"+com_id+"'><a class='mnkf_dialog_list_close'></a><div class='avatar'><img src='"+hz6d_coms_info[com_id].logo+"' name="+com_id+"><div class='status_icon'></div></div><div class='mnkf_nickname'></div><em class='arrow'></em></li>");
				}
				hz6d_is_chatting();
				dis_chatting_list(com_id);
			}else{//若已存在则选中该聊天
				if(type == "add") {
					flp("div.mnkf_list").show();
					flp("div.mnkf_content").show();
					flp("div.mnkf_talking").hide();
				}
				flp("div.mnkf_dialog_scroll li").removeClass("focus");
				flp("li[id=chat_com_"+com_id+"]").addClass("focus");
				hz6d_re_flashing(com_id);
				dis_com_card(com_id);
				if(!flp('#workers_info_div').is(':hidden')) dis_worker_card(com_id);
			}
			hz6d_new_chat(com_id,kf);
		 }
	});
}

//删除聊天窗口iframe
function close_chatting_iframe(com_id){
	hz6d_coms_info[com_id].voted = '0';
	hz6d_coms_info[com_id].linked = '0';
	sendDataToIframeProxy(com_id, 'close', String(Math.random()));
	flp("div.stay_altbox").remove();//删除鼠标停留弹出层
	hz6d_scrollDown();
	flp("ul.mnkf_content_head_icon li div.altbox").hide();
	var iframe_id = "hz6d_chat_iframe_"+com_id;
	setTimeout(function(){
		flp("#"+iframe_id).remove()
	},500);
}

//聊天闪烁
function hz6d_flashing(cid){
	var img = flp("div.mnkf_dialog_scroll li img[name="+cid+"]");
	var id = flp("div.mnkf_dialog_scroll li.focus").attr("id").replace('chat_com_','');
	if(img.attr("src")){//已建立则添加闪烁
		if(flp("div.mnkf_content").is(":hidden")){//对话框缩小时
			if(!HZ6D_CONFIGS.flashingInterval[cid]){
				HZ6D_CONFIGS.flashingInterval[cid] = setInterval("flp('div.mnkf_dialog_scroll li img[name="+cid+"]').fadeOut(150).fadeIn(150)",400);
			}
			if(!HZ6D_CONFIGS.newMsgInterval){
				flp('div.hz6d_bd div.newChat').html("&nbsp; &nbsp; 您有新消息");
				HZ6D_CONFIGS.newMsgInterval = setInterval("flp('div.hz6d_bd div.newChat').fadeOut(150).fadeIn(150)",400);
			}
		}else if(!flp("div.mnkf_content").is(":hidden")){
			if(cid != id){//未选中的id才会闪烁
				if(!HZ6D_CONFIGS.flashingInterval[cid]){
					HZ6D_CONFIGS.flashingInterval[cid] = setInterval("flp('div.mnkf_dialog_scroll li img[name="+cid+"]').fadeOut(150).fadeIn(150)",400);
				}
				if(!HZ6D_CONFIGS.newMsgInterval){
					flp('div.hz6d_bd div.newChat').html("&nbsp; &nbsp; 您有新消息");
					HZ6D_CONFIGS.newMsgInterval = setInterval("flp('div.hz6d_bd div.newChat').fadeOut(150).fadeIn(150)",400);
				}
			}
		}
	}else{//未建立则新建聊天
		add_chatting_list(cid,"flashing");
		if(!HZ6D_CONFIGS.flashingInterval[cid]){
			HZ6D_CONFIGS.flashingInterval[cid] = setInterval("flp('div.mnkf_dialog_scroll li img[name="+cid+"]').fadeOut(150).fadeIn(150)",400);
		}
		if(!HZ6D_CONFIGS.newMsgInterval){
			flp('div.hz6d_bd div.newChat').html("&nbsp; &nbsp; 您有新消息");
			HZ6D_CONFIGS.newMsgInterval = setInterval("flp('div.hz6d_bd div.newChat').fadeOut(150).fadeIn(150)",400);
		}
	}
	if(flp("#hz6d_mnkh_talking").css("display") == "none" && flp("#hz6d_mnkh_content").css("display") == "none"){
		flp("#hz6d_mnkh_talking").css("display","block");
	}
}

//打开聊天窗口取消选中聊天人闪烁
// function hz6d_clear_flashing(){
	// flp("div.newsbd").click(function(){
		// try{
			// var cid = flp("div.mnkf_dialog_scroll li.focus").attr("id").replace('chat_com_','');
			// hz6d_re_flashing(cid);
			// hz6d_is_chatting();
		// } catch(e){
		// }
	// });
// }

//缩小对话框
function hz6d_hideContent(){
	hz6d_is_chatting();
	flp("div.mnkf_content").hide();
	flp("div.mnkf_talking").show();
}

//显示对话框
function hz6d_showContent(){
	if(HZ6D_CONFIGS.firstopen == 1){
		hz6d_getCompanyInfo(HZ6D_CONFIGS.com_id, function(a){
			if (a == 1) {
				flp("div.l_title a.name").html(hz6d_coms_info[HZ6D_CONFIGS.com_id].nick || hz6d_coms_info[HZ6D_CONFIGS.com_id].name);
				//if(hz6d_coms_info[HZ6D_CONFIGS.com_id].site.indexOf("http://") == -1){
				//	flp("div.l_title a.l_title_t").attr("href","http://"+hz6d_coms_info[HZ6D_CONFIGS.com_id].site);
				//}else{
					flp("div.l_title a.l_title_t").attr("href",hz6d_coms_info[HZ6D_CONFIGS.com_id].site);
				//}
				flp("span.certification").removeAttr("title");
				if(hz6d_coms_info[HZ6D_CONFIGS.com_id].cert == 1){
					flp("span.certification").addClass("icon_approve");
					flp("span.certification").attr('title','企业身份认证');
				}
				hz6d_new_chat(HZ6D_CONFIGS.com_id);
				HZ6D_CONFIGS.firstopen = 0;
				HZ6D_CONFIGS.chatting_comid[0] = HZ6D_CONFIGS.com_id;
				//dis_com_card(HZ6D_CONFIGS.com_id);//公司
				//dis_worker_card(HZ6D_CONFIGS.com_id);//员工
				add_chatting_list(HZ6D_CONFIGS.com_id,'add');
				//dis_chatting_list(HZ6D_CONFIGS.com_id);//正在聊天列表
			}
		});
	}
	flp("div.mnkf_list").show();
	flp("div.mnkf_content").show();
	flp("div.mnkf_talking").hide();
}

//关闭对话框
function hz6d_closeContent(){
	hz6d_confirm("是否关闭所有对话?", 'hz6d_closeContent_act');
}

function hz6d_closeContent_act() {
	for(var i=0;i<HZ6D_CONFIGS.chatting_comid.length;i++){
		close_chatting_iframe(HZ6D_CONFIGS.chatting_comid[i]);
	}
	flp("div.mnkf_dialog_scroll li").remove();
	//var num = flp('.mnkf_dialog_scroll ul li').length;
	//flp('.mnkf_min').find('.hz6d_t').html('聊天 ('+num+')');
	flp("div.mnkf_content").hide();
	HZ6D_CONFIGS.firstopen = 1;
	flp('div.hz6d_bd div.newChat').html("&nbsp; &nbsp; &nbsp; 点击咨询");
	flp("div.mnkf_talking").show();
}

function hz6d_confirm(word, callback, params){
	var html = '<div class="mnkf_mbox_confirm">'+
		'<div class="mnkf_mbox_bd">' +
			'<div class="mnkf_mbox_bg">' +
				'<div class="mnkf_mbox_head">' +
					'<div class="mnkf_mbox_title">' + '友好提示' + '</div>' +
					'<a class="mnkf_mbox_close" onclick="hz6d_cancel(\'mnkf_mbox_confirm\');"></a>' +
				'</div>' +
				'<div class="mnkf_mbox_body">'+
					'<div style="padding:20px 20px 20px 20px;line-height:20px;">'+word+'</div>'+
				'</div>' +
				'<div class="mnkf_mbox_bottom">'+
					'<a class="greenBtn" onclick="' + callback + '(' + params + ');hz6d_cancel(\'mnkf_mbox_confirm\');">确定</a>　<a class="grayBtn" onclick="hz6d_cancel(\'mnkf_mbox_confirm\');">取消</a>&nbsp;&nbsp;'+
				'</div>'+
			'</div>' +
			//'<iframe style="position:absolute;top:0;left:0;width:100%;height:100%;filter:alpha(opacity=0);opacity:0;"></iframe>' +
		'</div>'+
	'</div>';
	flp('body').append(html);
	flp('.mnkf_mbox_confirm').css('width','300px');
}

function hz6d_alert(word, callback) {
	var html = '<div class="mnkf_mbox_confirm">'+
		'<div class="mnkf_mbox_bd">' +
			'<div class="mnkf_mbox_bg">' +
				'<div class="mnkf_mbox_head">' +
					'<div class="mnkf_mbox_title">' + '友好提示' + '</div>' +
					'<a class="mnkf_mbox_close" onclick="hz6d_cancel(\'mnkf_mbox_confirm\');"></a>' +
				'</div>' +
				'<div class="mnkf_mbox_body">'+
					'<div style="padding:20px 20px 20px 20px;line-height:20px;">'+word+'</div>'+
				'</div>' +
				'<div class="mnkf_mbox_bottom">'+
					'<a class="greenBtn" onclick="hz6d_cancel(\'mnkf_mbox_confirm\');">确定</a>&nbsp;&nbsp;'+
				'</div>'+
			'</div>' +
			//'<iframe scrolling="no" frameborder="0" style="position:absolute;top:0;left:0;width:100%;height:100%; background-color:transparent; z-index:-1;zoom:1"></iframe>' +
		'</div>'+
	'</div>';
	flp('body').append(html);
	flp('.mnkf_mbox_confirm').css('width','300px');
}

function hz6d_join_json(separator,json) {
	var arr = [];
	for (var i in json) {
		if (json.hasOwnProperty(i)) {
			arr.push(i + '=' + encodeURIComponent(json[i]));
		}
	}
	return arr.join(separator);
}