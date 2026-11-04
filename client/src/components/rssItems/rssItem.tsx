import styles from './rssItems.module.css'
import type { RssItem } from "../../api/rss"

export function RssItem({ data }: { data: RssItem | null }) {
    if (data === null) return <></>;

    return <>
        <div className={styles.rssItems}>
            <a className={styles.title} href={`/article/${data.id}`}>{data.name}</a>

            <div className={styles.meta}>
                <a href={`/feed/${data.feedId}`}>{data.feedName}</a>
                <time dateTime={data.publishedDateTime}>{data.publishedDateTime}</time>
            </div>

            <div dangerouslySetInnerHTML={{ __html: data.content! }} />
        </div>
    </>
}

