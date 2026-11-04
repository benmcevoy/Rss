import type { ReactNode } from 'react'
import styles from './two-column.module.css'

type Props = {
  aside: ReactNode
  children: ReactNode
}

export default function Layout({ aside, children }: Props) {
  return (
    <div className={styles.layout}>
      <aside className={styles.aside}>{aside}</aside>
      <main className={styles.main}>{children}</main>
    </div>
  )
}