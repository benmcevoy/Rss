import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import React from "react";
import TwoColumn from "./Layouts/TwoColumn/component";
import AppContext from "./AppContext";
import Config from "./config";

import TreeView from "./Components/TreeView/component.js";
import Menu from "./Components/Menu/component.js";
import List from "./Components/List/component.js";
import Item from "./Components/Item/component.js";

export default class App extends React.Component {
    constructor(props) {
        super(props)

        this.refresh = this.refresh.bind(this);
        this.markAsRead = this.markAsRead.bind(this);
        this.updateCurrent = this.updateCurrent.bind(this);
        this.fetchListData = this.fetchListData.bind(this);

        this.state = {
            current: {
                folder: null,
                feed: null,
                item: null
            },
            viewModel: null,
            id: null,
            updateCurrent: this.updateCurrent,
            refresh: this.refresh,
            markAsRead: this.markAsRead
        };

        this.fetchListData();
    }

    refresh = (type, id) => {
        console.log(`app.refresh: ${type ?? null}, ${id ?? null}`);
    };

    markAsRead = (type, id) => {
        console.log(`app.markAsRead: ${type ?? null}, ${id ?? null}`);
    };

    updateCurrent = (folderId, feedId, itemId) => {
        console.log(`app.updateCurrent: ${folderId ?? null}, ${feedId ?? null}, ${itemId ?? null}`);

        this.setState({
            current: {
                folder: folderId ?? null,
                feed: feedId ?? null,
                item: itemId ?? null
            }
        });
    }

    fetchListData() {
        const ctx = this.state.current;
        let url, id;

        if (ctx.item !== null) return;

        if (ctx.item === null && ctx.folder === null && ctx.feed === null) {
            id = "stream";
            url = Config.api.stream.get;
        }

        if (ctx.folder !== null && ctx.feed === null) {
            id = ctx.folder;
            url = Config.api.folder.get + `/${id}`;
        }

        if (ctx.folder !== null && ctx.feed !== null) {
            id = ctx.feed;
            url = Config.api.feed.get + `/${id}`;
        }

        // unchanged
        if (id === this.state.id) return;

        fetch(url)
            .then(x => x.json())
            .then(x => this.setState({ viewModel: x, id: id }));
    }

    componentDidMount = () => this.fetchListData();
	componentDidUpdate = () => this.fetchListData();

    render = () =>
        <AppContext.Provider value={this.state}>
            <TwoColumn
                header={<Menu />}
                aside={<TreeView title="Subscriptions" />}
                main={<div id="main-wrapper">
                    <List title="Stream" viewModel={this.state.viewModel} />
                    <Item title="Item" />
                </div>}
            />
        </AppContext.Provider>
}