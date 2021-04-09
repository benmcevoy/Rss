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
        this.fetchSubscription = this.fetchSubscription.bind(this);
        this.readItem = this.readItem.bind(this);
        this.subscribe = this.subscribe.bind(this);
        this.unsubscribe = this.unsubscribe.bind(this);

        this.state = {
            currentFolder: null,
            currentFeed: null,
            currentItem: null,

            listViewModel: null,
            listId: null,
            itemViewModel: null,
            itemId: null,
            subscriptionViewModel: null,
            subscriptionId: null,

            updateCurrent: this.updateCurrent,
            refresh: this.refresh,
            markAsRead: this.markAsRead,
            subscribe: this.subscribe,
            unsubscribe: this.unsubscribe
        };

        this.fetchSubscription();
    }

    refresh = (type, id) => {
        console.log(`app.refresh: ${type ?? null}, ${id ?? null}`);

        fetch(Config.api.refresh + `?type=${type}&id=${id ?? ""}`, { method: "POST" })
            // invalidate
            .then(x => this.setState({
                listViewModel: null,
                listId: null,
                itemViewModel: null,
                itemId: null,
                subscriptionId: null
            }));
    }

    markAsRead = (type, id) => {
        console.log(`app.markAsRead: ${type ?? null}, ${id ?? null}`);

        fetch(Config.api.markAsRead + `?type=${type}&id=${id ?? ""}`, { method: "POST" })
            // invalidate
            .then(x => this.setState({
                listViewModel: null,
                listId: null,
                itemViewModel: null,
                itemId: null,
                subscriptionId: null
            }));
    }

    subscribe = (id) => {
        console.log(`app.subscribe: ${id ?? null}`);
    }

    unsubscribe = (id) => {
        console.log(`app.unsubscribe: ${id ?? null}`);

        fetch(Config.api.unsubscribe + `?id=${id ?? ""}`, { method: "POST" })
            // invalidate
            .then(x => this.setState({
                currentFeed: null,
                currentItem: null,
                listViewModel: null,
                listId: null,
                itemViewModel: null,
                itemId: null,
                subscriptionId: null
            }));
    }

    updateCurrent = (folderId, feedId, itemId) => {
        console.log(`app.updateCurrent: ${folderId ?? null}, ${feedId ?? null}, ${itemId ?? null}`);

        this.setState({
            currentFolder: folderId ?? null,
            currentFeed: feedId ?? null,
            currentItem: itemId ?? null
        });
    }

    fetchListData = () => {
        let url, id;

        if (this.state.currentItem !== null) return;

        console.log(`app.fetchListData:`);

        if (this.state.currentItem === null && this.state.currentFolder === null && this.state.currentFeed === null) {
            id = "stream";
            url = Config.api.stream.get;
        }

        if (this.state.currentFolder !== null && this.state.currentFeed === null) {
            id = this.state.currentFolder;
            url = Config.api.folder.get + `/${id}`;
        }

        if (this.state.currentFolder !== null && this.state.currentFeed !== null) {
            id = this.state.currentFeed;
            url = Config.api.feed.get + `/${id}`;
        }

        // unchanged
        if (id === this.state.listId) return;

        fetch(url)
            .then(x => x.json())
            .then(x => this.setState({
                listViewModel: x,
                listId: id,
                itemViewModel: null,
                itemId: null
            }));
    }

    fetchSubscription = () => {
        // unchanged
        if (this.state.subscriptionId !== null) return;

        console.log(`app.fetchSubscription:`);

        fetch(Config.api.subscription.get)
            .then(x => x.json())
            .then(x => this.setState({
                subscriptionViewModel: x,
                subscriptionId: "Set"
            }));
    }

    readItem = () => {
        console.log(`app.readItem:`);

        let url, id;

        if (this.state.currentItem === null) return;

        id = this.state.currentItem;
        url = Config.api.item.read + `?id=${id}`;

        // unchanged
        if (id === this.state.itemId) return;

        fetch(url, { method: "POST" })
            .then(x => x.json())
            .then(x => this.setState({
                itemViewModel: x,
                itemId: id,
                listViewModel: null,
                listId: null,
                subscriptionId: null
            }));
    }

    // equivalent to useEffect
    componentDidUpdate = () => {
        this.fetchListData();
        this.readItem();
        this.fetchSubscription();
    }

    render = () =>
        <AppContext.Provider value={this.state}>
            <TwoColumn
                header={<Menu />}
                aside={<TreeView title="Subscriptions" viewModel={this.state.subscriptionViewModel} />}
                main={<div id="main-wrapper">
                    <List title="Stream" viewModel={this.state.listViewModel} />
                    <Item title="Item" viewModel={this.state.itemViewModel} />
                </div>}
            />
        </AppContext.Provider>
}