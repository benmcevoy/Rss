import "./style.css"
import React from "react";
import AppContext from "../../AppContext";

export default function TreeView(props) {
	const viewModel = props.viewModel ?? null;

	if (viewModel === null) {
		return (<div class="component" id="treeview"><h3>Loading...</h3></div>);
	}

	return (
		<AppContext.Consumer>
			{(ctx) => (
				<div class="component" id="treeview">
					<h3><button class="link-button" onClick={() => ctx.updateCurrent()}>{props.title || "Tree view"}</button></h3>
					<nav>
						<ul class="treeview-root">
							{viewModel.folders.map((folder) => {
								const folderSelected = folder.id === ctx.currentFolder ? "treeview-folder selected" : "treeview-folder";
								// TODO: recursion would tidy this up
								return (
									<li class={folderSelected}><button class="link-button" onClick={() => ctx.updateCurrent(folder.id)}>{folder.name} ({folder.count})</button>
										<ul>{
											folder.feeds.map((feed) => {
												const itemSelected = feed.id === ctx.currentFeed ? "treeview-item selected" : "treeview-item";
												return (<li class={itemSelected}>
													<button class="link-button" onClick={() => ctx.updateCurrent(folder.id, feed.id)}>{feed.name} ({feed.count})</button>
												</li>)
											})}
										</ul>
									</li>)
							})}
						</ul>
						<ul class="treeview-items">
							{viewModel.feeds.map((feed) => {
								const itemSelected = feed.id === ctx.currentFeed ? "treeview-item selected" : "treeview-item";
								return (<li class={itemSelected}>
									<button class="link-button" onClick={() => ctx.updateCurrent(null, feed.id)}>{feed.name} ({feed.count})</button>
								</li>)
							})}
						</ul>
					</nav>
				</div>
			)}
		</AppContext.Consumer>
	);
}
