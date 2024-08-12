<script setup>
import {computed} from 'vue'

import Tree from 'vuejs-tree'
import { useCategoryStore } from '@/stores/categories'
const categoryStore = useCategoryStore()
//https://openbase.com/js/vuejs-tree/documentation

// let treeDisplayData = [
//   {
//     text: 'Root 1',
//     state: { checked: false, selected: false, expanded: false },
//     nodes: [
//       {
//         text: 'Child 1',
//         state: { checked: false, selected: false, expanded: false }
//       }
//     ]
//   },
//   {
//     text: 'Root 2',
//     state: { checked: false, selected: false, expanded: false }
//   }
// ]
const topLevelCategories = computed(() => {
  return categoryStore.categories.filter((category) => category.parentCategoryId == 0)
})
const categoryTree = computed(() => {
  return topLevelCategories.value.map((category) => {
    category.nodes = BuildTree(category)
    category = convertCategoryView(category)
    return category
  })
})

function BuildTree(parentNode) {
  var children = categories.filter((c) => c.parentCategoryId == parentNode.id)
  children.forEach((childNode) => {
    childNode.nodes = BuildTree(childNode)
  })
  return children.map((child) => {
    return convertCategoryView(child)
  })
}
function convertCategoryView(category) {
  category.state = {
    checked: false,
    expanded: false,
    selected: false
  }
  category.text = category.name
  return category
}
</script>

<template>
  <div class="container">
    <h1>Categories</h1>
    <Tree :nodes="categoryTree" />
  </div>
</template>