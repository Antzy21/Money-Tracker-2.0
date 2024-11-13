import { createRouter, createWebHistory } from 'vue-router'
import Categories from '@/views/Categories.vue'
import HomeView from '@/views/Home.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomeView
  },
  {
    path: '/categories',
    name: 'Categories',
    component: Categories
  },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router
