<script lang="ts">
  import type { Snippet } from "svelte";

  const { children }: {
    children: Snippet;
  } = $props();
</script>

<div class="flex">
  <div class="text-4xl mx-auto">
    {@render children()}
  </div>
</div>
