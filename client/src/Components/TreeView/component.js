import "./style.css"
import React from "react";
import AppContext from "../../AppContext";

export default class TreeView extends React.Component {
	render() {
		const viewModel = this.props.viewModel ?? null;

		if (viewModel === null) {
			return (<div class="component" id="treeview"><h3>Loading...</h3></div>);
		}

		return (
			<AppContext.Consumer>
				{(ctx) => (
					<div class="component" id="treeview">
						<h3><a onClick={() => ctx.updateCurrent()}>{this.props.title || "Tree view"}</a></h3>
						<nav>
							<ul class="treeview-root">
								{viewModel.folders.map((folder) => {
									const folderSelected = folder.id === ctx.current.folder ? "treeview-folder selected" : "treeview-folder";
									// TODO: recursion would tidy this up
									return (
										<li class={folderSelected}><a onClick={() => ctx.updateCurrent(folder.id)}>{folder.name} ({folder.count})</a>
											<ul>{
												folder.feeds.map((feed) => {
													const itemSelected = feed.id === ctx.current.feed ? "treeview-item selected" : "treeview-item";
													return (<li class={itemSelected} onClick={() => ctx.updateCurrent(folder.id, feed.id)}>{feed.name} ({feed.count})</li>)
												})}
											</ul>
										</li>)
								})}
							</ul>
							<ul class="treeview-items">
								{viewModel.feeds.map((feed) => {
									const itemSelected = feed.id === ctx.current.feed ? "treeview-item selected" : "treeview-item";
									return (<li class={itemSelected}><a onClick={() => ctx.updateCurrent(null, feed.id)}>{feed.name} ({feed.count})</a></li>)
								})}
							</ul>
						</nav>
					</div>
				)}
			</AppContext.Consumer>
		);
	}
}
