webpackJsonp([21],{Nnw2:function(t,e,a){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var n={render:function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("el-dialog",{attrs:{title:"日志列表","close-on-click-modal":!1,visible:t.visible,width:"75%"},on:{"update:visible":function(e){t.visible=e}}},[a("el-form",{attrs:{inline:!0,model:t.dataForm},nativeOn:{keyup:function(e){if(!("button"in e)&&t._k(e.keyCode,"enter",13,e.key,"Enter"))return null;t.getDataList()}}},[a("el-form-item",[a("el-input",{attrs:{placeholder:"任务ID",clearable:""},model:{value:t.dataForm.id,callback:function(e){t.$set(t.dataForm,"id",e)},expression:"dataForm.id"}})],1),t._v(" "),a("el-form-item",[a("el-button",{on:{click:function(e){t.getDataList()}}},[t._v("查询")])],1)],1),t._v(" "),a("el-table",{directives:[{name:"loading",rawName:"v-loading",value:t.dataListLoading,expression:"dataListLoading"}],staticStyle:{width:"100%"},attrs:{data:t.dataList,border:"",height:"460"}},[a("el-table-column",{attrs:{prop:"logId","header-align":"center",align:"center",width:"80",label:"日志ID"}}),t._v(" "),a("el-table-column",{attrs:{prop:"jobId","header-align":"center",align:"center",width:"80",label:"任务ID"}}),t._v(" "),a("el-table-column",{attrs:{prop:"beanName","header-align":"center",align:"center",label:"bean名称"}}),t._v(" "),a("el-table-column",{attrs:{prop:"params","header-align":"center",align:"center",label:"参数"}}),t._v(" "),a("el-table-column",{attrs:{prop:"status","header-align":"center",align:"center",label:"状态"},scopedSlots:t._u([{key:"default",fn:function(e){return[0===e.row.status?a("el-tag",{attrs:{size:"small"}},[t._v("成功")]):a("el-tag",{staticStyle:{cursor:"pointer"},attrs:{size:"small",type:"danger"},nativeOn:{click:function(a){t.showErrorInfo(e.row.logId)}}},[t._v("失败")])]}}])}),t._v(" "),a("el-table-column",{attrs:{prop:"times","header-align":"center",align:"center",label:"耗时(单位: 毫秒)"}}),t._v(" "),a("el-table-column",{attrs:{prop:"createTime","header-align":"center",align:"center",width:"180",label:"执行时间"}})],1),t._v(" "),a("el-pagination",{attrs:{"current-page":t.pageIndex,"page-sizes":[10,20,50,100],"page-size":t.pageSize,total:t.totalPage,layout:"total, sizes, prev, pager, next, jumper"},on:{"size-change":t.sizeChangeHandle,"current-change":t.currentChangeHandle}})],1)},staticRenderFns:[]},i=a("VU/8")({data:function(){return{visible:!1,dataForm:{id:""},dataList:[],pageIndex:1,pageSize:10,totalPage:0,dataListLoading:!1}},methods:{init:function(){this.visible=!0,this.getDataList()},getDataList:function(){var t=this;this.dataListLoading=!0,this.$http({url:this.$http.adornUrl("/sys/scheduleLog/list"),method:"get",params:this.$http.adornParams({page:this.pageIndex,limit:this.pageSize,jobId:this.dataForm.id})}).then(function(e){var a=e.data;a&&0===a.code?(t.dataList=a.page.list,t.totalPage=a.page.totalCount):(t.dataList=[],t.totalPage=0),t.dataListLoading=!1})},sizeChangeHandle:function(t){this.pageSize=t,this.pageIndex=1,this.getDataList()},currentChangeHandle:function(t){this.pageIndex=t,this.getDataList()},showErrorInfo:function(t){var e=this;this.$http({url:this.$http.adornUrl("/sys/scheduleLog/info/"+t),method:"get",params:this.$http.adornParams()}).then(function(t){var a=t.data;a&&0===a.code?e.$alert(a.log.error):e.$message.error(a.msg)})}}},n,!1,null,null,null);e.default=i.exports}});